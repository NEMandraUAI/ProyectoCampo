using BE;
using BLL;
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
    public partial class frmSesion : Form
    {
        RegistroBLL registroBLL = new RegistroBLL();
        UsuarioBLL usuarioBLL = new UsuarioBLL();
        UsuarioBE usuarioActual;
        public event EventHandler CerrarSesion;
        public frmSesion(UsuarioBE usuarioP)
        {
            InitializeComponent();
            usuarioActual = usuarioP;
            this.FormClosing += FrmSesion_FormClosing;
        }
        private void Sesion_Load(object sender, EventArgs e)
        {
            lblBienvenida.Text = "Sesión activa - Bienvenido, " + usuarioActual.Nombre;
            dgvLogs.AutoGenerateColumns = false;
            ConfigurarGrilla();
            CargarComboUsuarios();
            cmbCriticidad.SelectedIndex = 0;
            dtpDesde.Value = DateTime.Now.AddDays(-7);
            dtpHasta.Value = DateTime.Now;
            EjecutarFiltro();
            SessionManager.Instancia.PermisosActualizados += SessionManager_PermisosActualizados;
            AplicarPermisosVisuales();
        }
        private void SessionManager_PermisosActualizados(object sender, EventArgs e)
        {
            usuarioActual = SessionManager.Instancia.UsuarioActual;
            AplicarPermisosVisuales();
        }
        private void AplicarPermisosVisuales()
        {
            btnGestionRoles.Visible = usuarioActual.TienePermiso("GESTION_ROLES");
            btnControlCambios.Visible = usuarioActual.TienePermiso("CONTROL_BITACORA");
            panel1.Visible = usuarioActual.TienePermiso("CONTROL_BITACORA");
            dgvLogs.Visible = usuarioActual.TienePermiso("CONTROL_BITACORA");
            btnBackup.Visible = usuarioActual.TienePermiso("REALIZAR_BACKUP");
        }
        private void ConfigurarGrilla()
        {
            dgvLogs.Columns.Clear();
            dgvLogs.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "FechaHora", HeaderText = "Fecha", Width = 130 });
            dgvLogs.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NombreUsuario", HeaderText = "Usuario", Width = 100 });
            dgvLogs.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Criticidad", HeaderText = "Criticidad", Width = 90 });
            dgvLogs.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Evento", HeaderText = "Detalle del Evento", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvLogs.CellFormatting += DgvLogs_CellFormatting;
        }
        private void CargarComboUsuarios()
        {
            List<UsuarioBE> listaUsuarios = usuarioBLL.ListarTodos();
            listaUsuarios.Insert(0, new UsuarioBE { ID = 0, Nombre = "Todos" });
            cmbUsuarios.DataSource = listaUsuarios;
            cmbUsuarios.DisplayMember = "Nombre";
            cmbUsuarios.ValueMember = "ID";
        }
        private void EjecutarFiltro()
        {
            int? idUsu = null;
            if (cmbUsuarios.SelectedValue != null && (int)cmbUsuarios.SelectedValue > 0)
            {
                idUsu = (int)cmbUsuarios.SelectedValue;
            }
            dgvLogs.DataSource = null;
            dgvLogs.DataSource = registroBLL.ConsultaLogsFiltros(
                dtpDesde.Value,
                dtpHasta.Value,
                cmbCriticidad.Text,
                idUsu,
                txtBuscar.Text);
        }
        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            EjecutarFiltro();
        }
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            dtpDesde.Value = DateTime.Now.AddDays(-7);
            dtpHasta.Value = DateTime.Now;
            cmbCriticidad.SelectedIndex = 0;
            cmbUsuarios.SelectedIndex = 0;
            txtBuscar.Text = "";
            EjecutarFiltro();
        }
        private void DgvLogs_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvLogs.Rows[e.RowIndex].DataBoundItem is RegistroBE fila)
            {
                if (fila.Criticidad == "ALERTA")
                {
                    dgvLogs.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightYellow;
                }
                else if (fila.Criticidad == "CRÍTICO")
                {
                    dgvLogs.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightCoral;
                    dgvLogs.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
                }
                else if (fila.Criticidad == "ALTA")
                {
                    dgvLogs.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGreen;
                }
            }
        }
        private void FrmSesion_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (SessionManager.Instancia.UsuarioActual != null)
            {
                SessionManager.Instancia.PermisosActualizados -= SessionManager_PermisosActualizados;
                usuarioBLL.CerrarSesion();
                MessageBox.Show("La sesión se cerró correctamente.", "Sesión Cerrada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CerrarSesion?.Invoke(this, EventArgs.Empty);
            }
        }
        private void btnControlCambios_Click(object sender, EventArgs e)
        {
            frmControlCambios frmCambios = new frmControlCambios();
            frmCambios.MdiParent = this.MdiParent;
            frmCambios.FormClosed += (s, args) =>
            {
                this.Show();
            };
            this.Hide();
            frmCambios.Show();
        }
        private void btnGestionRoles_Click(object sender, EventArgs e)
        {
            frmGestionRoles frmGestion = new frmGestionRoles();
            frmGestion.MdiParent = this.MdiParent;
            frmGestion.FormClosed += (s, args) =>
            {
                this.Show();
            };
            this.Hide();
            frmGestion.Show();
        }
        private void btnBackup_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Archivos de Backup SQL (*.bak)|*.bak";
            sfd.Title = "Guardar Copia de Seguridad";
            sfd.FileName = $"ProyectoCampo_Backup_{DateTime.Now:yyyyMMdd_HHmmss}.bak";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    BackupBLL backupBLL = new BackupBLL();
                    backupBLL.RealizarBackup(sfd.FileName);
                    MessageBox.Show("Copia de seguridad generada y almacenada con éxito.", "Backup Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al generar el backup: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
