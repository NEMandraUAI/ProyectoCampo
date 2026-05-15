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
    }
}
