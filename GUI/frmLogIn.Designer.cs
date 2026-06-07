namespace GUI
{
    partial class frmLogIn
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtUsuario = new TextBox();
            txtClave = new TextBox();
            btnIniciarSesion = new Button();
            lblUsuario = new Label();
            lblClave = new Label();
            btnVerClave = new Button();
            SuspendLayout();
            // 
            // txtUsuario
            // 
            txtUsuario.Location = new Point(35, 37);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(194, 23);
            txtUsuario.TabIndex = 0;
            // 
            // txtClave
            // 
            txtClave.Location = new Point(35, 104);
            txtClave.Name = "txtClave";
            txtClave.PasswordChar = '*';
            txtClave.Size = new Size(194, 23);
            txtClave.TabIndex = 1;
            // 
            // btnIniciarSesion
            // 
            btnIniciarSesion.Location = new Point(35, 217);
            btnIniciarSesion.Name = "btnIniciarSesion";
            btnIniciarSesion.Size = new Size(194, 44);
            btnIniciarSesion.TabIndex = 2;
            btnIniciarSesion.Text = "Iniciar Sesion";
            btnIniciarSesion.UseVisualStyleBackColor = true;
            btnIniciarSesion.Click += btnIniciarSesion_Click_1;
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Location = new Point(35, 19);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(47, 15);
            lblUsuario.TabIndex = 3;
            lblUsuario.Text = "Usuario";
            // 
            // lblClave
            // 
            lblClave.AutoSize = true;
            lblClave.Location = new Point(35, 86);
            lblClave.Name = "lblClave";
            lblClave.Size = new Size(67, 15);
            lblClave.TabIndex = 4;
            lblClave.Text = "Contraseña";
            // 
            // btnVerClave
            // 
            btnVerClave.Location = new Point(35, 167);
            btnVerClave.Name = "btnVerClave";
            btnVerClave.Size = new Size(194, 44);
            btnVerClave.TabIndex = 5;
            btnVerClave.Text = "Ver Clave";
            btnVerClave.UseVisualStyleBackColor = true;
            btnVerClave.Click += btnVerClave_Click;
            // 
            // frmLogIn
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(273, 273);
            Controls.Add(btnVerClave);
            Controls.Add(lblClave);
            Controls.Add(lblUsuario);
            Controls.Add(btnIniciarSesion);
            Controls.Add(txtClave);
            Controls.Add(txtUsuario);
            Name = "frmLogIn";
            Text = "LogIn";
            Load += frmLogIn_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtUsuario;
        private TextBox txtClave;
        private Button btnIniciarSesion;
        private Label lblUsuario;
        private Label lblClave;
        private Button btnVerClave;
    }
}
