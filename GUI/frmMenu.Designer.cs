namespace GUI
{
    partial class frmMenu
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
            menuStrip1 = new MenuStrip();
            iniciarSesionToolStripMenuItem = new ToolStripMenuItem();
            iniciarSesiónToolStripMenuItem = new ToolStripMenuItem();
            salirToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { iniciarSesionToolStripMenuItem, salirToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // iniciarSesionToolStripMenuItem
            // 
            iniciarSesionToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { iniciarSesiónToolStripMenuItem });
            iniciarSesionToolStripMenuItem.Name = "iniciarSesionToolStripMenuItem";
            iniciarSesionToolStripMenuItem.Size = new Size(53, 20);
            iniciarSesionToolStripMenuItem.Text = "Sesión";
            // 
            // iniciarSesiónToolStripMenuItem
            // 
            iniciarSesiónToolStripMenuItem.Name = "iniciarSesiónToolStripMenuItem";
            iniciarSesiónToolStripMenuItem.Size = new Size(180, 22);
            iniciarSesiónToolStripMenuItem.Text = "Iniciar Sesión";
            iniciarSesiónToolStripMenuItem.Click += iniciarSesiónToolStripMenuItem_Click;
            // 
            // salirToolStripMenuItem
            // 
            salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            salirToolStripMenuItem.Size = new Size(41, 20);
            salirToolStripMenuItem.Text = "Salir";
            salirToolStripMenuItem.Click += salirToolStripMenuItem_Click;
            // 
            // frmMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(menuStrip1);
            IsMdiContainer = true;
            MainMenuStrip = menuStrip1;
            Name = "frmMenu";
            Text = "frmMenu";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem iniciarSesionToolStripMenuItem;
        private ToolStripMenuItem iniciarSesiónToolStripMenuItem;
        private ToolStripMenuItem salirToolStripMenuItem;
    }
}