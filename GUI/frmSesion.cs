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
            CargarLogs();
        }

        public void CargarLogs()
        {
            dgvLogs.DataSource = null;
            dgvLogs.DataSource = registroBLL.ConsultarLogs();
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            registroBLL.RegistrarEvento("Cierre de sesión del usuario: " + usuarioActual.Nombre, usuarioActual);
            usuarioBLL.CerrarSesion();
            MessageBox.Show("La sesión se cerró correctamente.", "Sesión Cerrada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CerrarSesion?.Invoke(this, EventArgs.Empty);
        }
    }
}
