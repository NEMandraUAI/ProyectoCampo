namespace GUI
{
    partial class frmRegistro
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtUsuario = new TextBox();
            txtClave1 = new TextBox();
            txtClave2 = new TextBox();
            btnRegistrar = new Button();
            lblUsuario = new Label();
            lblClave1 = new Label();
            lblClave2 = new Label();
            btnVerClave = new Button();
            SuspendLayout();
            // 
            // txtUsuario
            // 
            txtUsuario.Location = new Point(36, 39);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(183, 23);
            txtUsuario.TabIndex = 0;
            // 
            // txtClave1
            // 
            txtClave1.Location = new Point(36, 98);
            txtClave1.Name = "txtClave1";
            txtClave1.PasswordChar = '*';
            txtClave1.Size = new Size(183, 23);
            txtClave1.TabIndex = 1;
            // 
            // txtClave2
            // 
            txtClave2.Location = new Point(36, 157);
            txtClave2.Name = "txtClave2";
            txtClave2.PasswordChar = '*';
            txtClave2.Size = new Size(183, 23);
            txtClave2.TabIndex = 2;
            // 
            // btnRegistrar
            // 
            btnRegistrar.Location = new Point(36, 253);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.Size = new Size(183, 47);
            btnRegistrar.TabIndex = 3;
            btnRegistrar.Text = "Registrar";
            btnRegistrar.UseVisualStyleBackColor = true;
            btnRegistrar.Click += btnRegistrar_Click;
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Location = new Point(36, 21);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(110, 15);
            lblUsuario.TabIndex = 4;
            lblUsuario.Text = "Nombre de Usuario";
            // 
            // lblClave1
            // 
            lblClave1.AutoSize = true;
            lblClave1.Location = new Point(36, 80);
            lblClave1.Name = "lblClave1";
            lblClave1.Size = new Size(67, 15);
            lblClave1.TabIndex = 5;
            lblClave1.Text = "Contraseña";
            // 
            // lblClave2
            // 
            lblClave2.AutoSize = true;
            lblClave2.Location = new Point(36, 139);
            lblClave2.Name = "lblClave2";
            lblClave2.Size = new Size(107, 15);
            lblClave2.TabIndex = 6;
            lblClave2.Text = "Repetir Contraseña";
            // 
            // btnVerClave
            // 
            btnVerClave.Location = new Point(36, 200);
            btnVerClave.Name = "btnVerClave";
            btnVerClave.Size = new Size(183, 47);
            btnVerClave.TabIndex = 7;
            btnVerClave.Text = "Ver Clave";
            btnVerClave.UseVisualStyleBackColor = true;
            btnVerClave.Click += btnVerClave_Click;
            // 
            // frmRegistro
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(261, 312);
            Controls.Add(btnVerClave);
            Controls.Add(lblClave2);
            Controls.Add(lblClave1);
            Controls.Add(lblUsuario);
            Controls.Add(btnRegistrar);
            Controls.Add(txtClave2);
            Controls.Add(txtClave1);
            Controls.Add(txtUsuario);
            Name = "frmRegistro";
            Text = "Registro";
            Load += frmRegistro_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtUsuario;
        private TextBox txtClave1;
        private TextBox txtClave2;
        private Button btnRegistrar;
        private Label lblUsuario;
        private Label lblClave1;
        private Label lblClave2;
        private Button btnVerClave;
    }
}