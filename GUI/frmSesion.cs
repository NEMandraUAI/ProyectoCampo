using BE;
using BLL;
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
            usuarioBLL.CerrarSesion();
            MessageBox.Show("La sesión se cerró correctamente.", "Sesión Cerrada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CerrarSesion?.Invoke(this, EventArgs.Empty);
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
    }
}
