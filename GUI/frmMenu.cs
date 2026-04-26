using BE;
using GUI.Eventos;
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
            AbrirLogin();
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
            foreach (Form f in this.MdiChildren)
            {
                f.Close();
            }

            AbrirLogin();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void registrarseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRegistro registro = new frmRegistro();
            registro.MdiParent = this;
            registro.Show();
        }
    }
}
