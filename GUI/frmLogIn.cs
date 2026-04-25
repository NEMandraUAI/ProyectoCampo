using BE;
using BLL;

namespace GUI
{
    public partial class frmLogIn : Form
    {
        UsuarioBLL usuarioBLL = new UsuarioBLL();
        RegistroBLL registroBLL = new RegistroBLL();

        public frmLogIn()
        {
            InitializeComponent();
        }

        private void btnIniciarSesion_Click_1(object sender, EventArgs e)
        {
            UsuarioBE usuarioLogueado = usuarioBLL.IniciarSesion(txtUsuario.Text, txtClave.Text);

            if (usuarioLogueado != null)
            {
                registroBLL.RegistrarEvento("Inicio de sesión del usuario: " + usuarioLogueado.Nombre, usuarioLogueado);
                frmSesion frmSesion = new frmSesion(usuarioLogueado);
                frmSesion.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Usuario o clave incorrectos.");
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
