using BE;
using BLL;
using GUI.Eventos;

namespace GUI
{
    public partial class frmLogIn : Form
    {
        UsuarioBLL usuarioBLL = new UsuarioBLL();
        RegistroBLL registroBLL = new RegistroBLL();
        public event EventHandler<LogInEventArgs> LoginExitoso;

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
                LoginExitoso?.Invoke(this, new LogInEventArgs(usuarioLogueado));
            }
            else
            {
                MessageBox.Show("Usuario o clave incorrectos.");
            }
        }
    }
}
