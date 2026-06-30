namespace GUI
{
    partial class frmControlCambios
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
            cmbUsuarios = new ComboBox();
            dgvHistorial = new DataGridView();
            btnRestaurar = new Button();
            btnVolver = new Button();
            btnModificarJerarquia = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvHistorial).BeginInit();
            SuspendLayout();
            // 
            // cmbUsuarios
            // 
            cmbUsuarios.FormattingEnabled = true;
            cmbUsuarios.Location = new Point(32, 62);
            cmbUsuarios.Name = "cmbUsuarios";
            cmbUsuarios.Size = new Size(187, 23);
            cmbUsuarios.TabIndex = 0;
            cmbUsuarios.SelectedIndexChanged += cmbUsuarios_SelectedIndexChanged;
            // 
            // dgvHistorial
            // 
            dgvHistorial.AllowUserToAddRows = false;
            dgvHistorial.AllowUserToDeleteRows = false;
            dgvHistorial.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvHistorial.Location = new Point(32, 117);
            dgvHistorial.Name = "dgvHistorial";
            dgvHistorial.ReadOnly = true;
            dgvHistorial.Size = new Size(736, 310);
            dgvHistorial.TabIndex = 1;
            // 
            // btnRestaurar
            // 
            btnRestaurar.Location = new Point(248, 55);
            btnRestaurar.Name = "btnRestaurar";
            btnRestaurar.Size = new Size(141, 34);
            btnRestaurar.TabIndex = 2;
            btnRestaurar.Text = "Restaurar";
            btnRestaurar.UseVisualStyleBackColor = true;
            btnRestaurar.Click += btnRestaurar_Click;
            // 
            // btnVolver
            // 
            btnVolver.Location = new Point(579, 433);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(189, 39);
            btnVolver.TabIndex = 5;
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = true;
            btnVolver.Click += btnVolver_Click;
            // 
            // btnModificarJerarquia
            // 
            btnModificarJerarquia.Location = new Point(410, 55);
            btnModificarJerarquia.Name = "btnModificarJerarquia";
            btnModificarJerarquia.Size = new Size(141, 34);
            btnModificarJerarquia.TabIndex = 6;
            btnModificarJerarquia.Text = "Modificar Jerarquía";
            btnModificarJerarquia.UseVisualStyleBackColor = true;
            btnModificarJerarquia.Click += btnModificarJerarquia_Click;
            // 
            // frmControlCambios
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 480);
            Controls.Add(btnModificarJerarquia);
            Controls.Add(btnVolver);
            Controls.Add(btnRestaurar);
            Controls.Add(dgvHistorial);
            Controls.Add(cmbUsuarios);
            Name = "frmControlCambios";
            Text = "Control de Cambios";
            FormClosing += frmControlCambios_FormClosing;
            Load += frmControlCambios_Load;
            ((System.ComponentModel.ISupportInitialize)dgvHistorial).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ComboBox cmbUsuarios;
        private DataGridView dgvHistorial;
        private Button btnRestaurar;
        private Button btnVolver;
        private Button btnModificarJerarquia;
    }
}