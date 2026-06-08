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
    public partial class frmControlCambios : Form, IObserverIdioma
    {
        private Dictionary<string, string> _traducciones = new Dictionary<string, string>();
        private string T(string clave, string textoPorDefecto)
        {
            return _traducciones != null && _traducciones.ContainsKey(clave) ? _traducciones[clave] : textoPorDefecto;
        }
        UsuarioBLL usuarioBLL = new UsuarioBLL();
        public frmControlCambios()
        {
            InitializeComponent();
            GestorIdioma.Instancia.Suscribir(this);
        }
        private void frmControlCambios_Load(object sender, EventArgs e)
        {
            ConfigurarGrilla();
            CargarUsuarios();
            ActualizarIdioma(GestorIdioma.Instancia.IdiomaActual);
        }
        private void ConfigurarGrilla()
        {
            dgvHistorial.AutoGenerateColumns = false;
            dgvHistorial.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHistorial.MultiSelect = false;
            dgvHistorial.ReadOnly = true;
            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "FechaHora", HeaderText = "Fecha del Cambio", Width = 140 });
            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Accion", HeaderText = "Acción / Motivo", Width = 200 });
            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NombreUsuarioAutor", HeaderText = "Modificado Por", Width = 120 });
            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Nombre", HeaderText = "Nombre Histórico", Width = 120 });
            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "IntentosFallidos", HeaderText = "Intentos", Width = 70 });
            dgvHistorial.Columns.Add(new DataGridViewCheckBoxColumn { DataPropertyName = "Bloqueado", HeaderText = "Bloqueado", Width = 70 });
        }
        private void CargarUsuarios()
        {
            List<UsuarioBE> usuarios = usuarioBLL.ListarTodos();
            cmbUsuarios.DataSource = usuarios;
            cmbUsuarios.DisplayMember = "Nombre";
            cmbUsuarios.ValueMember = "ID";
            cmbUsuarios.SelectedIndex = -1;
        }
        public void ActualizarIdioma(IdiomaBE idioma)
        {
            IdiomaBLL idiomaBLL = new IdiomaBLL();
            _traducciones = idiomaBLL.ObtenerTraducciones(idioma, this.Name);
            if (_traducciones.ContainsKey(this.Name))
                this.Text = _traducciones[this.Name];
            TraducirControlesRecursivo(this.Controls, _traducciones);
        }
        private void TraducirControlesRecursivo(Control.ControlCollection controles, Dictionary<string, string> traducciones)
        {
            foreach (Control control in controles)
            {
                if (traducciones.ContainsKey(control.Name))
                {
                    control.Text = traducciones[control.Name];
                }
                if (control is MenuStrip menuStrip)
                {
                    foreach (ToolStripItem item in menuStrip.Items)
                    {
                        TraducirItemMenu(item, traducciones);
                    }
                }
                if (control.HasChildren)
                {
                    TraducirControlesRecursivo(control.Controls, traducciones);
                }
                if (control is DataGridView dgv)
                {
                    foreach (DataGridViewColumn columna in dgv.Columns)
                    {
                        if (traducciones.ContainsKey(columna.DataPropertyName))
                        {
                            columna.HeaderText = traducciones[columna.DataPropertyName];
                        }
                    }
                }
            }
        }
        private void TraducirItemMenu(ToolStripItem item, Dictionary<string, string> traducciones)
        {
            if (traducciones.ContainsKey(item.Name))
            {
                item.Text = traducciones[item.Name];
            }
            if (item is ToolStripMenuItem menuItem)
            {
                foreach (ToolStripItem subItem in menuItem.DropDownItems)
                {
                    TraducirItemMenu(subItem, traducciones);
                }
            }
        }
        private void cmbUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbUsuarios.SelectedValue != null && cmbUsuarios.SelectedValue is int)
            {
                int idUsuarioSeleccionado = (int)cmbUsuarios.SelectedValue;
                CargarHistorial(idUsuarioSeleccionado);
            }
        }
        private void CargarHistorial(int idUsuario)
        {
            try
            {
                var historial = usuarioBLL.ObtenerHistorialUsuario(idUsuario);
                dgvHistorial.DataSource = null;
                dgvHistorial.DataSource = historial;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, T("msgErrorHistorial", "Error al cargar historial"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            if (dgvHistorial.CurrentRow == null || !(dgvHistorial.CurrentRow.DataBoundItem is UsuarioHistoricoBE))
            {
                MessageBox.Show(T("msgSeleccionarEstado", "Debe seleccionar un estado histórico de la grilla para restaurar."), T("titAtencion", "Atención"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            UsuarioHistoricoBE estadoSeleccionado = (UsuarioHistoricoBE)dgvHistorial.CurrentRow.DataBoundItem;
            int idUsuario = (int)cmbUsuarios.SelectedValue;
            DialogResult respuesta = MessageBox.Show(T("msgConfirmarRestauracion", "¿Está seguro que desea restaurar al usuario a la versión del ") + estadoSeleccionado.FechaHora + "?", T("titConfirmarRestauracion", "Confirmar Restauración"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta == DialogResult.Yes)
            {
                try
                {
                    UsuarioBE autor = SessionManager.Instancia.UsuarioActual;
                    usuarioBLL.RestaurarEstadoUsuario(idUsuario, estadoSeleccionado.ID_Cambio, autor);
                    MessageBox.Show(T("msgRestauracionExito", "El estado del usuario ha sido restaurado exitosamente. Se ha registrado el cambio en la auditoría."), T("titExito", "Éxito"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarHistorial(idUsuario);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, T("msgErrorRestauracion", "Error en la restauración"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void frmControlCambios_FormClosing(object sender, FormClosingEventArgs e)
        {
            GestorIdioma.Instancia.Desuscribir(this);
        }
    }
}
