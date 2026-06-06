using BE;
using GUI.Eventos;
using Seguridad;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmMenu : Form
    {
        public frmMenu()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += FrmMenu_KeyDown;
        }
        private void FrmMenu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Alt && e.KeyCode == Keys.R)
            {
                string claveIngresada = SolicitarClaveEmergencia();
                if (!string.IsNullOrEmpty(claveIngresada))
                {
                    string hashClaveMaestra = "3b612c75a7b5048a435fb6ec81e52ff92d6d795a8b5a9c17070f6a63c97a53b2";
                    if (CryptoManager.Comparar(claveIngresada, hashClaveMaestra))
                    {
                        DialogResult opcion = MessageBox.Show(
                        "Autenticación Maestra correcta.\n\n" +
                        "¿Desea RECALCULAR toda la estructura de dígitos (SÍ) " +
                        "o RESTAURAR un archivo de Backup (NO)?\n\n" +
                        "Presione Cancelar para abortar la operación.",
                        "Opciones de Recuperación de Emergencia",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Question);
                        if (opcion == DialogResult.Yes)
                        {
                            try
                            {
                                BLL.IntegridadBLL integridadBLL = new BLL.IntegridadBLL();
                                integridadBLL.ForzarRecalculoDeTodaLaBase();
                                MessageBox.Show("Integridad restaurada exitosamente. Los dígitos verificadores han sido recalculados.",
                                                "Recuperación Exitosa",
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Information);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Ocurrió un error al intentar recalcular: " + ex.Message,
                                                "Error Crítico",
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Error);
                            }
                        }
                        else if (opcion == DialogResult.No)
                        {
                            OpenFileDialog ofd = new OpenFileDialog();
                            ofd.Filter = "Archivos de Backup SQL (*.bak)|*.bak";
                            ofd.Title = "Seleccione el archivo de respaldo para restaurar";
                            if (ofd.ShowDialog() == DialogResult.OK)
                            {
                                try
                                {
                                    BLL.BackupBLL backupBLL = new BLL.BackupBLL();
                                    backupBLL.RestaurarBackup(ofd.FileName);
                                    MessageBox.Show("El sistema se ha restaurado correctamente al punto en el tiempo seleccionado. Por favor, inicie sesión nuevamente.",
                                                    "Restauración Exitosa",
                                                    MessageBoxButtons.OK,
                                                    MessageBoxIcon.Information);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Ocurrió un error al intentar restaurar la base de datos: " + ex.Message,
                                                    "Error Crítico",
                                                    MessageBoxButtons.OK,
                                                    MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Clave maestra incorrecta. Acceso denegado.",
                                        "Seguridad",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                    }
                }
            }
        }
        private void iniciarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BLL.IntegridadBLL integridadBLL = new BLL.IntegridadBLL();
            // integridadBLL.ForzarRecalculoDeTodaLaBase(); 
            // MessageBox.Show("Base de datos sincronizada correctamente.");
            // return;
            try
            {
                integridadBLL.VerificarIntegridadSistema();
                if (SessionManager.Instancia.UsuarioActual != null)
                {
                    MessageBox.Show("Ya existe una sesión activa. Debe cerrarla antes de ingresar con otra cuenta.", "Sesión Activa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                AbrirLogin();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nEl sistema se bloqueará por seguridad. Contacte al administrador.", "Corrupción de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                foreach (Form f in this.MdiChildren)
                {
                    f.Close();
                }
            }
        }
        private void AbrirLogin()
        {
            frmLogIn login = new frmLogIn();
            login.MdiParent = this;
            login.LoginExitoso += OnLoginExitoso;
            login.Show();
        }
        private void OnLoginExitoso(object sender, LogInEventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                f.Close();
            }
            AbrirSesion(e.Usuario);
        }
        private void AbrirSesion(UsuarioBE pUsuario)
        {
            frmSesion bienvenida = new frmSesion(pUsuario);
            bienvenida.MdiParent = this;
            bienvenida.CerrarSesion += OnCerrarSesion;
            bienvenida.Show();
        }
        private void OnCerrarSesion(object sender, EventArgs e)
        {
            BLL.IntegridadBLL integridadBLL = new BLL.IntegridadBLL();
            try
            {
                integridadBLL.VerificarIntegridadSistema();
                AbrirLogin();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nEl sistema se bloqueará por seguridad. Contacte al administrador.", "Corrupción de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                foreach (Form f in this.MdiChildren)
                {
                    f.Close();
                }
            }
        }
        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void registrarseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BLL.IntegridadBLL integridadBLL = new BLL.IntegridadBLL();
            try
            {
                integridadBLL.VerificarIntegridadSistema();
                frmRegistro registro = new frmRegistro();
                registro.MdiParent = this;
                registro.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nEl sistema se bloqueará por seguridad. Contacte al administrador.", "Corrupción de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                foreach (Form f in this.MdiChildren)
                {
                    f.Close();
                }
            }
        }
        private string SolicitarClaveEmergencia()
        {
            Form prompt = new Form()
            {
                Width = 350,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Recuperación de Emergencia del Sistema",
                StartPosition = FormStartPosition.CenterScreen,
                MaximizeBox = false,
                MinimizeBox = false
            };
            Label lblTexto = new Label() { Left = 20, Top = 20, Width = 300, Text = "Ingrese la Clave Maestra de Administrador:" };
            TextBox txtClave = new TextBox() { Left = 20, Top = 50, Width = 290, PasswordChar = '*' };
            Button btnAceptar = new Button() { Text = "Aceptar", Left = 210, Width = 100, Top = 80, DialogResult = DialogResult.OK };
            prompt.Controls.Add(lblTexto);
            prompt.Controls.Add(txtClave);
            prompt.Controls.Add(btnAceptar);
            prompt.AcceptButton = btnAceptar;
            if (prompt.ShowDialog() == DialogResult.OK)
            {
                return txtClave.Text;
            }
            return "";
        }
    }
}
