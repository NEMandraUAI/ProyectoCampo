using BE;
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
    public partial class frmGestionRoles : Form
    {
        PermisoBLL permisoBLL = new PermisoBLL();
        UsuarioBLL usuarioBLL = new UsuarioBLL();
        List<ComponentePermiso> listaArbolCompleto;
        FamiliaBE familiaSeleccionada;
        public frmGestionRoles()
        {
            InitializeComponent();
        }
        private void frmGestionRoles_Load(object sender, EventArgs e)
        {
            CargarArbolRoles();
            CargarUsuarios();
        }
        private void CargarArbolRoles()
        {
            tvRoles.Nodes.Clear();
            listaArbolCompleto = permisoBLL.ObtenerArbolCompleto();
            foreach (var comp in listaArbolCompleto)
            {
                TreeNode nodo = new TreeNode(comp.Nombre);
                nodo.Tag = comp;
                tvRoles.Nodes.Add(nodo);
                MostrarRecursivo(nodo, comp);
            }
            tvRoles.ExpandAll();
        }
        private void MostrarRecursivo(TreeNode nodoPadre, ComponentePermiso componente)
        {
            if (componente.ObtenerHijos() == null) return;
            foreach (var hijo in componente.ObtenerHijos())
            {
                TreeNode nodoHijo = new TreeNode(hijo.Nombre);
                nodoHijo.Tag = hijo;
                nodoPadre.Nodes.Add(nodoHijo);
                MostrarRecursivo(nodoHijo, hijo);
            }
        }
        private void CargarUsuarios()
        {
            cmbUsuarios.DataSource = usuarioBLL.ListarTodos();
            cmbUsuarios.DisplayMember = "Nombre";
            cmbUsuarios.ValueMember = "ID";
        }
        private void btnCrearFamilia_Click(object sender, EventArgs e)
        {
            string nombreRol = Microsoft.VisualBasic.Interaction.InputBox("Nombre de la nueva familia (Rol):", "Nuevo Rol", "");
            if (!string.IsNullOrWhiteSpace(nombreRol))
            {
                FamiliaBE nuevaFamilia = new FamiliaBE { Nombre = nombreRol };
                try
                {
                    permisoBLL.GuardarFamiliaCompleta(nuevaFamilia);
                    MessageBox.Show("Rol creado. Ahora selecciónelo y añada permisos.");
                    CargarArbolRoles();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }
        private void btnEliminarFamilia_Click(object sender, EventArgs e)
        {
            if (tvRoles.SelectedNode != null && tvRoles.SelectedNode.Tag is FamiliaBE fam)
            {
                try
                {
                    permisoBLL.EliminarFamilia(fam);
                    CargarArbolRoles();
                    MessageBox.Show("Rol eliminado del sistema.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Eliminación denegada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        private void cmbUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbUsuarios.SelectedItem is UsuarioBE user)
            {
                permisoBLL.LlenarPermisosDeUsuario(user);
                tvPermisosUsuario.Nodes.Clear();

                foreach (var p in user.Permisos)
                {
                    TreeNode nodo = new TreeNode(p.Nombre);
                    tvPermisosUsuario.Nodes.Add(nodo);
                    if (p is FamiliaBE) MostrarRecursivo(nodo, p);
                }
                tvPermisosUsuario.ExpandAll();
            }
        }
        private void btnAsignarRolUsuario_Click(object sender, EventArgs e)
        {
            if (cmbUsuarios.SelectedItem is UsuarioBE user && tvRoles.SelectedNode != null)
            {
                ComponentePermiso rolAsignar = (ComponentePermiso)tvRoles.SelectedNode.Tag;
                if (!user.Permisos.Exists(x => x.ID == rolAsignar.ID))
                {
                    user.Permisos.Add(rolAsignar);
                    try
                    {
                        permisoBLL.ActualizarPermisosUsuario(user);
                        cmbUsuarios_SelectedIndexChanged(null, null);
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                }
            }
        }
        private void btnQuitarRolUsuario_Click(object sender, EventArgs e)
        {
            if (cmbUsuarios.SelectedItem is UsuarioBE user && tvPermisosUsuario.SelectedNode != null)
            {
                string nombreQuitar = tvPermisosUsuario.SelectedNode.Text;
                var permisoAQuitar = user.Permisos.FirstOrDefault(x => x.Nombre == nombreQuitar);

                if (permisoAQuitar != null)
                {
                    user.Permisos.Remove(permisoAQuitar);
                    try
                    {
                        permisoBLL.ActualizarPermisosUsuario(user);
                        cmbUsuarios_SelectedIndexChanged(null, null);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cmbUsuarios_SelectedIndexChanged(null, null);
                    }
                }
            }
        }
    }
}
