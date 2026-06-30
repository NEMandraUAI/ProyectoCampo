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
    public partial class frmGestionIdiomas : Form, IObserverIdioma
    {
        private Dictionary<string, string> _traducciones = new Dictionary<string, string>();
        private string T(string clave, string textoPorDefecto)
        {
            return _traducciones != null && _traducciones.ContainsKey(clave) ? _traducciones[clave] : textoPorDefecto;
        }
        private Label lblSeleccion;
        private ComboBox cmbIdiomasAdmin;
        private Button btnNuevo;
        private Button btnEliminar;
        private Button btnGuardar;
        private DataGridView dgvTraducciones;
        private DataGridViewTextBoxColumn colFormulario;
        private DataGridViewTextBoxColumn colNombreControl;
        private DataGridViewTextBoxColumn colTexto;
        private IdiomaBLL idiomaBLL = new IdiomaBLL();
        public frmGestionIdiomas()
        {
            InitializeComponent();
            ConfigurarUI();
            GestorIdioma.Instancia.Suscribir(this);
            this.Load += frmGestionIdiomas_Load;
            this.FormClosing += frmGestionIdiomas_FormClosing;
        }
        private void ConfigurarUI()
        {
            this.Name = "frmGestionIdiomas";
            this.Size = new Size(720, 510);
            this.StartPosition = FormStartPosition.CenterScreen;
            lblSeleccion = new Label { Location = new Point(20, 25), AutoSize = true, Name = "lblSeleccion" };
            cmbIdiomasAdmin = new ComboBox { Location = new Point(150, 20), Width = 200, DropDownStyle = ComboBoxStyle.DropDownList, Name = "cmbIdiomasAdmin" };
            cmbIdiomasAdmin.SelectedIndexChanged += CmbIdiomasAdmin_SelectedIndexChanged;
            btnNuevo = new Button { Location = new Point(370, 18), Width = 110, Name = "btnNuevo" };
            btnNuevo.Click += BtnNuevo_Click;
            btnEliminar = new Button { Location = new Point(490, 18), Width = 110, Name = "btnEliminar" };
            btnEliminar.Click += BtnEliminar_Click;
            dgvTraducciones = new DataGridView
            {
                Location = new Point(20, 60),
                Size = new Size(660, 340),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoGenerateColumns = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                Name = "dgvTraducciones"
            };
            colFormulario = new DataGridViewTextBoxColumn { DataPropertyName = "Formulario", ReadOnly = true, Width = 150, Name = "colFormulario" };
            colNombreControl = new DataGridViewTextBoxColumn { DataPropertyName = "NombreControl", ReadOnly = true, Width = 150, Name = "colNombreControl" };
            colTexto = new DataGridViewTextBoxColumn { DataPropertyName = "Texto", Width = 310, Name = "colTexto" };
            dgvTraducciones.Columns.AddRange(new DataGridViewColumn[] { colFormulario, colNombreControl, colTexto });
            btnGuardar = new Button { Location = new Point(440, 415), Width = 240, Height = 40, Name = "btnGuardar" };
            btnGuardar.Click += BtnGuardar_Click;
            this.Controls.Add(lblSeleccion);
            this.Controls.Add(cmbIdiomasAdmin);
            this.Controls.Add(btnNuevo);
            this.Controls.Add(btnEliminar);
            this.Controls.Add(dgvTraducciones);
            this.Controls.Add(btnGuardar);
        }
        private void frmGestionIdiomas_Load(object sender, EventArgs e)
        {
            CargarIdiomas();
            ActualizarIdioma(GestorIdioma.Instancia.IdiomaActual);
        }
        public void ActualizarIdioma(IdiomaBE idioma)
        {
            if (idioma == null) return;
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
                if (control.HasChildren)
                {
                    TraducirControlesRecursivo(control.Controls, traducciones);
                }
                if (control is DataGridView dgv)
                {
                    foreach (DataGridViewColumn columna in dgv.Columns)
                    {
                        if (traducciones.ContainsKey(columna.Name))
                        {
                            columna.HeaderText = traducciones[columna.Name];
                        }
                    }
                }
            }
        }
        private void CargarIdiomas()
        {
            cmbIdiomasAdmin.SelectedIndexChanged -= CmbIdiomasAdmin_SelectedIndexChanged;
            cmbIdiomasAdmin.DataSource = idiomaBLL.ObtenerIdiomas();
            cmbIdiomasAdmin.DisplayMember = "Nombre";
            cmbIdiomasAdmin.ValueMember = "ID";
            cmbIdiomasAdmin.SelectedIndexChanged += CmbIdiomasAdmin_SelectedIndexChanged;
            if (cmbIdiomasAdmin.Items.Count > 0)
                CargarTraducciones((int)cmbIdiomasAdmin.SelectedValue);
        }
        private void CmbIdiomasAdmin_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbIdiomasAdmin.SelectedValue != null)
                CargarTraducciones((int)cmbIdiomasAdmin.SelectedValue);
        }
        private void CargarTraducciones(int idIdioma)
        {
            dgvTraducciones.DataSource = idiomaBLL.ObtenerTodasLasTraducciones(idIdioma);
        }
        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            string nuevoNombre = Microsoft.VisualBasic.Interaction.InputBox(T("msgInputNuevo", "Ingrese el nombre del nuevo idioma:"), T("titInputNuevo", "Nuevo Idioma"));
            string sufijo = Microsoft.VisualBasic.Interaction.InputBox(T("msgInputSufijo", "Ingrese un sufijo identificador temporal (Ej: ENG):"), T("titInputSufijo", "Sufijo"));
            if (!string.IsNullOrEmpty(nuevoNombre) && !string.IsNullOrEmpty(sufijo))
            {
                try
                {
                    idiomaBLL.AgregarNuevoIdioma(nuevoNombre, sufijo);
                    MessageBox.Show(T("msgNuevoExito", "Idioma generado con las etiquetas base. Ahora puede editarlas en la grilla."), T("titExito", "Éxito"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarIdiomas();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, T("titError", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (cmbIdiomasAdmin.SelectedValue != null)
            {
                int idIdioma = (int)cmbIdiomasAdmin.SelectedValue;
                if (MessageBox.Show(T("msgConfirmarEliminar", "¿Eliminar este idioma? Los usuarios que lo tengan volverán a Español."), T("titConfirmarEliminar", "Advertencia"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        idiomaBLL.EliminarIdioma(idIdioma);
                        MessageBox.Show(T("msgEliminarExito", "Idioma eliminado correctamente."), T("titExito", "Éxito"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (GestorIdioma.Instancia.IdiomaActual.ID == idIdioma)
                        {
                            var espanol = idiomaBLL.ObtenerIdiomas().FirstOrDefault(i => i.ID == 1);
                            if (espanol != null) GestorIdioma.Instancia.CambiarIdioma(espanol);
                        }
                        CargarIdiomas();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, T("titError", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (dgvTraducciones.DataSource is List<TraduccionBE> lista)
            {
                try
                {
                    idiomaBLL.ActualizarTraducciones(lista);
                    MessageBox.Show(T("msgGuardarExito", "Las traducciones se han guardado exitosamente."), T("titGuardarExito", "Guardado"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (cmbIdiomasAdmin.SelectedValue != null && (int)cmbIdiomasAdmin.SelectedValue == GestorIdioma.Instancia.IdiomaActual.ID)
                    {
                        GestorIdioma.Instancia.Notificar();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, T("titError", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void frmGestionIdiomas_FormClosing(object sender, FormClosingEventArgs e)
        {
            GestorIdioma.Instancia.Desuscribir(this);
        }
    }
}
