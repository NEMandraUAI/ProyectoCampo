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
    public partial class frmRegistro : Form
    {
        UsuarioBLL usuarioBLL = new UsuarioBLL();
        public frmRegistro()
        {
            InitializeComponent();
            txtClave1.PasswordChar = '*';
            txtClave2.PasswordChar = '*';
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsuario.Text) || string.IsNullOrWhiteSpace(txtClave1.Text) || string.IsNullOrWhiteSpace(txtClave2.Text))
            {
                MessageBox.Show("Debe completar todos los campos.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtClave1.Text != txtClave2.Text)
            {
                MessageBox.Show("Las contraseñas no coinciden.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                usuarioBLL.RegistrarUsuario(txtUsuario.Text, txtClave1.Text);
                MessageBox.Show("Usuario registrado con éxito.", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVerClave_Click(object sender, EventArgs e)
        {
            if (txtClave1.PasswordChar == '*' && txtClave2.PasswordChar == '*')
            {
                txtClave1.PasswordChar = '\0';
                txtClave2.PasswordChar = '\0';
                btnVerClave.Text = "Ocultar Clave";
            }
            else
            {
                txtClave1.PasswordChar = '*';
                txtClave2.PasswordChar = '*';
                btnVerClave.Text = "Ver Clave";
            }
        }
    }
}
