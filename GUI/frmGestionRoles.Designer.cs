namespace GUI
{
    partial class frmGestionRoles
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
            tvRoles = new TreeView();
            cmbUsuarios = new ComboBox();
            tvPermisosUsuario = new TreeView();
            btnCrearFamilia = new Button();
            btnEliminarFamilia = new Button();
            btnAsignarRolUsuario = new Button();
            btnQuitarRolUsuario = new Button();
            btnAsignarPermisoARol = new Button();
            btnCrearRolAnidado = new Button();
            tvPermisosDisponibles = new TreeView();
            lblJerarquia = new Label();
            lblUsuario = new Label();
            lblPermisosUsuario = new Label();
            lblCatalogo = new Label();
            SuspendLayout();
            // 
            // tvRoles
            // 
            tvRoles.HideSelection = false;
            tvRoles.Location = new Point(12, 27);
            tvRoles.Name = "tvRoles";
            tvRoles.Size = new Size(322, 314);
            tvRoles.TabIndex = 0;
            // 
            // cmbUsuarios
            // 
            cmbUsuarios.FormattingEnabled = true;
            cmbUsuarios.Location = new Point(340, 45);
            cmbUsuarios.Name = "cmbUsuarios";
            cmbUsuarios.Size = new Size(207, 23);
            cmbUsuarios.TabIndex = 1;
            cmbUsuarios.SelectedIndexChanged += cmbUsuarios_SelectedIndexChanged;
            // 
            // tvPermisosUsuario
            // 
            tvPermisosUsuario.Location = new Point(553, 27);
            tvPermisosUsuario.Name = "tvPermisosUsuario";
            tvPermisosUsuario.Size = new Size(322, 314);
            tvPermisosUsuario.TabIndex = 2;
            // 
            // btnCrearFamilia
            // 
            btnCrearFamilia.Location = new Point(372, 431);
            btnCrearFamilia.Name = "btnCrearFamilia";
            btnCrearFamilia.Size = new Size(142, 52);
            btnCrearFamilia.TabIndex = 3;
            btnCrearFamilia.Text = "Crear Rol";
            btnCrearFamilia.UseVisualStyleBackColor = true;
            btnCrearFamilia.Click += btnCrearFamilia_Click;
            // 
            // btnEliminarFamilia
            // 
            btnEliminarFamilia.Location = new Point(372, 547);
            btnEliminarFamilia.Name = "btnEliminarFamilia";
            btnEliminarFamilia.Size = new Size(142, 52);
            btnEliminarFamilia.TabIndex = 5;
            btnEliminarFamilia.Text = "Eliminar Rol";
            btnEliminarFamilia.UseVisualStyleBackColor = true;
            btnEliminarFamilia.Click += btnEliminarFamilia_Click;
            // 
            // btnAsignarRolUsuario
            // 
            btnAsignarRolUsuario.Location = new Point(372, 95);
            btnAsignarRolUsuario.Name = "btnAsignarRolUsuario";
            btnAsignarRolUsuario.Size = new Size(142, 52);
            btnAsignarRolUsuario.TabIndex = 6;
            btnAsignarRolUsuario.Text = "Asignar Rol/Permiso a Usuario";
            btnAsignarRolUsuario.UseVisualStyleBackColor = true;
            btnAsignarRolUsuario.Click += btnAsignarRolUsuario_Click;
            // 
            // btnQuitarRolUsuario
            // 
            btnQuitarRolUsuario.Location = new Point(372, 153);
            btnQuitarRolUsuario.Name = "btnQuitarRolUsuario";
            btnQuitarRolUsuario.Size = new Size(142, 52);
            btnQuitarRolUsuario.TabIndex = 7;
            btnQuitarRolUsuario.Text = "Quitar Rol/Permiso a Usuario";
            btnQuitarRolUsuario.UseVisualStyleBackColor = true;
            btnQuitarRolUsuario.Click += btnQuitarRolUsuario_Click;
            // 
            // btnAsignarPermisoARol
            // 
            btnAsignarPermisoARol.Location = new Point(12, 347);
            btnAsignarPermisoARol.Name = "btnAsignarPermisoARol";
            btnAsignarPermisoARol.Size = new Size(142, 52);
            btnAsignarPermisoARol.TabIndex = 9;
            btnAsignarPermisoARol.Text = "Asignar Rol/Permiso a Rol";
            btnAsignarPermisoARol.UseVisualStyleBackColor = true;
            btnAsignarPermisoARol.Click += btnAsignarPatenteARol_Click;
            // 
            // btnCrearRolAnidado
            // 
            btnCrearRolAnidado.Location = new Point(372, 489);
            btnCrearRolAnidado.Name = "btnCrearRolAnidado";
            btnCrearRolAnidado.Size = new Size(142, 52);
            btnCrearRolAnidado.TabIndex = 10;
            btnCrearRolAnidado.Text = "Crear Rol Anidado";
            btnCrearRolAnidado.UseVisualStyleBackColor = true;
            btnCrearRolAnidado.Click += btnCrearRolAnidado_Click;
            // 
            // tvPermisosDisponibles
            // 
            tvPermisosDisponibles.HideSelection = false;
            tvPermisosDisponibles.Location = new Point(12, 431);
            tvPermisosDisponibles.Name = "tvPermisosDisponibles";
            tvPermisosDisponibles.Size = new Size(322, 314);
            tvPermisosDisponibles.TabIndex = 11;
            // 
            // lblJerarquia
            // 
            lblJerarquia.AutoSize = true;
            lblJerarquia.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblJerarquia.Location = new Point(12, 9);
            lblJerarquia.Name = "lblJerarquia";
            lblJerarquia.Size = new Size(278, 15);
            lblJerarquia.TabIndex = 12;
            lblJerarquia.Text = "Estructura Jerárquica de Roles (Árbol de Familias)";
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblUsuario.Location = new Point(340, 27);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(116, 15);
            lblUsuario.TabIndex = 13;
            lblUsuario.Text = "Seleccionar Usuario";
            // 
            // lblPermisosUsuario
            // 
            lblPermisosUsuario.AutoSize = true;
            lblPermisosUsuario.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPermisosUsuario.Location = new Point(553, 9);
            lblPermisosUsuario.Name = "lblPermisosUsuario";
            lblPermisosUsuario.Size = new Size(252, 15);
            lblPermisosUsuario.TabIndex = 14;
            lblPermisosUsuario.Text = "Permisos Efectivos del Usuario Seleccionado";
            // 
            // lblCatalogo
            // 
            lblCatalogo.AutoSize = true;
            lblCatalogo.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCatalogo.Location = new Point(12, 413);
            lblCatalogo.Name = "lblCatalogo";
            lblCatalogo.Size = new Size(280, 15);
            lblCatalogo.TabIndex = 15;
            lblCatalogo.Text = "Catálogo General de Permisos y Roles Disponibles";
            // 
            // frmGestionRoles
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(907, 756);
            Controls.Add(lblCatalogo);
            Controls.Add(lblPermisosUsuario);
            Controls.Add(lblUsuario);
            Controls.Add(lblJerarquia);
            Controls.Add(tvPermisosDisponibles);
            Controls.Add(btnCrearRolAnidado);
            Controls.Add(btnAsignarPermisoARol);
            Controls.Add(btnQuitarRolUsuario);
            Controls.Add(btnAsignarRolUsuario);
            Controls.Add(btnEliminarFamilia);
            Controls.Add(btnCrearFamilia);
            Controls.Add(tvPermisosUsuario);
            Controls.Add(cmbUsuarios);
            Controls.Add(tvRoles);
            Name = "frmGestionRoles";
            Text = "Gestion de Roles";
            Load += frmGestionRoles_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TreeView tvRoles;
        private ComboBox cmbUsuarios;
        private TreeView tvPermisosUsuario;
        private Button btnCrearFamilia;
        private Button btnEliminarFamilia;
        private Button btnAsignarRolUsuario;
        private Button btnQuitarRolUsuario;
        private ListBox lstPatentes;
        private Button btnAsignarPermisoARol;
        private Button btnCrearRolAnidado;
        private TreeView tvPermisosDisponibles;
        private Label lblJerarquia;
        private Label lblUsuario;
        private Label lblPermisosUsuario;
        private Label lblCatalogo;
    }
}