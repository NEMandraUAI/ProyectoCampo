using BE;
using BLL;
using GUI.Eventos;

namespace GUI
{
    public partial class frmLogIn : Form
    {
        UsuarioBLL usuarioBLL = new UsuarioBLL();
        public event EventHandler<LogInEventArgs> LoginExitoso;

        public frmLogIn()
        {
            InitializeComponent();
        }

        private void btnIniciarSesion_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsuario.Text) || string.IsNullOrWhiteSpace(txtClave.Text))
            {
                MessageBox.Show("Por favor, complete ambos campos para iniciar sesión.", "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                UsuarioBE usuarioLogueado = usuarioBLL.IniciarSesion(txtUsuario.Text, txtClave.Text);
                LoginExitoso?.Invoke(this, new LogInEventArgs(usuarioLogueado));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de Autenticación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
