namespace GUI
{
    partial class frmSesion
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
            lblBienvenida = new Label();
            btnCerrarSesion = new Button();
            dgvLogs = new DataGridView();
            panel1 = new Panel();
            lblBuscar = new Label();
            lblCriticidad = new Label();
            lblHasta = new Label();
            lblUsuarios = new Label();
            lblDesde = new Label();
            btnLimpiar = new Button();
            btnFiltrar = new Button();
            txtBuscar = new TextBox();
            cmbUsuarios = new ComboBox();
            cmbCriticidad = new ComboBox();
            dtpHasta = new DateTimePicker();
            dtpDesde = new DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)dgvLogs).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // lblBienvenida
            // 
            lblBienvenida.AutoSize = true;
            lblBienvenida.Location = new Point(12, 69);
            lblBienvenida.Name = "lblBienvenida";
            lblBienvenida.Size = new Size(151, 15);
            lblBienvenida.TabIndex = 0;
            lblBienvenida.Text = "Sesión activa - Bienvenido, ";
            // 
            // btnCerrarSesion
            // 
            btnCerrarSesion.Location = new Point(51, 127);
            btnCerrarSesion.Name = "btnCerrarSesion";
            btnCerrarSesion.Size = new Size(135, 50);
            btnCerrarSesion.TabIndex = 1;
            btnCerrarSesion.Text = "Cerrar Sesion";
            btnCerrarSesion.UseVisualStyleBackColor = true;
            btnCerrarSesion.Click += btnCerrarSesion_Click;
            // 
            // dgvLogs
            // 
            dgvLogs.AllowUserToAddRows = false;
            dgvLogs.AllowUserToDeleteRows = false;
            dgvLogs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLogs.Location = new Point(12, 202);
            dgvLogs.MultiSelect = false;
            dgvLogs.Name = "dgvLogs";
            dgvLogs.ReadOnly = true;
            dgvLogs.RowHeadersVisible = false;
            dgvLogs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLogs.Size = new Size(936, 330);
            dgvLogs.TabIndex = 2;
            // 
            // panel1
            // 
            panel1.Controls.Add(lblBuscar);
            panel1.Controls.Add(lblCriticidad);
            panel1.Controls.Add(lblHasta);
            panel1.Controls.Add(lblUsuarios);
            panel1.Controls.Add(lblDesde);
            panel1.Controls.Add(btnLimpiar);
            panel1.Controls.Add(btnFiltrar);
            panel1.Controls.Add(txtBuscar);
            panel1.Controls.Add(cmbUsuarios);
            panel1.Controls.Add(cmbCriticidad);
            panel1.Controls.Add(dtpHasta);
            panel1.Controls.Add(dtpDesde);
            panel1.Location = new Point(339, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(609, 184);
            panel1.TabIndex = 3;
            // 
            // lblBuscar
            // 
            lblBuscar.AutoSize = true;
            lblBuscar.Location = new Point(3, 124);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new Size(94, 15);
            lblBuscar.TabIndex = 10;
            lblBuscar.Text = "Buscar por Texto";
            // 
            // lblCriticidad
            // 
            lblCriticidad.AutoSize = true;
            lblCriticidad.Location = new Point(3, 65);
            lblCriticidad.Name = "lblCriticidad";
            lblCriticidad.Size = new Size(58, 15);
            lblCriticidad.TabIndex = 9;
            lblCriticidad.Text = "Criticidad";
            // 
            // lblHasta
            // 
            lblHasta.AutoSize = true;
            lblHasta.Location = new Point(308, 6);
            lblHasta.Name = "lblHasta";
            lblHasta.Size = new Size(37, 15);
            lblHasta.TabIndex = 8;
            lblHasta.Text = "Hasta";
            // 
            // lblUsuarios
            // 
            lblUsuarios.AutoSize = true;
            lblUsuarios.Location = new Point(308, 65);
            lblUsuarios.Name = "lblUsuarios";
            lblUsuarios.Size = new Size(52, 15);
            lblUsuarios.TabIndex = 7;
            lblUsuarios.Text = "Usuarios";
            // 
            // lblDesde
            // 
            lblDesde.AutoSize = true;
            lblDesde.Location = new Point(3, 6);
            lblDesde.Name = "lblDesde";
            lblDesde.Size = new Size(39, 15);
            lblDesde.TabIndex = 6;
            lblDesde.Text = "Desde";
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(449, 127);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(135, 50);
            btnLimpiar.TabIndex = 5;
            btnLimpiar.Text = "Limpiar Filtros";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // btnFiltrar
            // 
            btnFiltrar.Location = new Point(308, 127);
            btnFiltrar.Name = "btnFiltrar";
            btnFiltrar.Size = new Size(135, 50);
            btnFiltrar.TabIndex = 4;
            btnFiltrar.Text = "Aplicar Filtros";
            btnFiltrar.UseVisualStyleBackColor = true;
            btnFiltrar.Click += btnFiltrar_Click;
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(3, 142);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(276, 23);
            txtBuscar.TabIndex = 4;
            // 
            // cmbUsuarios
            // 
            cmbUsuarios.FormattingEnabled = true;
            cmbUsuarios.Items.AddRange(new object[] { "Todos", "INFO", "ALERTA", "CRÍTICO" });
            cmbUsuarios.Location = new Point(308, 83);
            cmbUsuarios.Name = "cmbUsuarios";
            cmbUsuarios.Size = new Size(276, 23);
            cmbUsuarios.TabIndex = 3;
            // 
            // cmbCriticidad
            // 
            cmbCriticidad.FormattingEnabled = true;
            cmbCriticidad.Items.AddRange(new object[] { "Todos", "INFO", "ALERTA", "CRÍTICO" });
            cmbCriticidad.Location = new Point(3, 83);
            cmbCriticidad.Name = "cmbCriticidad";
            cmbCriticidad.Size = new Size(276, 23);
            cmbCriticidad.TabIndex = 2;
            // 
            // dtpHasta
            // 
            dtpHasta.Location = new Point(308, 24);
            dtpHasta.Name = "dtpHasta";
            dtpHasta.Size = new Size(276, 23);
            dtpHasta.TabIndex = 1;
            // 
            // dtpDesde
            // 
            dtpDesde.Location = new Point(3, 24);
            dtpDesde.Name = "dtpDesde";
            dtpDesde.Size = new Size(276, 23);
            dtpDesde.TabIndex = 0;
            // 
            // frmSesion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(959, 544);
            Controls.Add(panel1);
            Controls.Add(dgvLogs);
            Controls.Add(btnCerrarSesion);
            Controls.Add(lblBienvenida);
            Name = "frmSesion";
            Text = "Sesion";
            Load += Sesion_Load;
            ((System.ComponentModel.ISupportInitialize)dgvLogs).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblBienvenida;
        private Button btnCerrarSesion;
        private DataGridView dgvLogs;
        private Panel panel1;
        private Button btnFiltrar;
        private TextBox txtBuscar;
        private ComboBox cmbUsuarios;
        private ComboBox cmbCriticidad;
        private DateTimePicker dtpHasta;
        private DateTimePicker dtpDesde;
        private Label lblBuscar;
        private Label lblCriticidad;
        private Label lblHasta;
        private Label lblUsuarios;
        private Label lblDesde;
        private Button btnLimpiar;
    }
}