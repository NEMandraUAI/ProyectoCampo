using BE;
using BLL;
using Seguridad;
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
            var arbolEstructural = permisoBLL.ObtenerArbolJerarquicoVisual();
            LlenarTreeView(tvRoles, arbolEstructural);
            listaArbolCompleto = permisoBLL.ObtenerArbolCompleto();
            CargarCatalogoPermisosDisponibles();
        }
        private void CargarCatalogoPermisosDisponibles()
        {
            tvPermisosDisponibles.Nodes.Clear();
            TreeNode nodoRoles = new TreeNode("ROLES (Familias)") { ForeColor = Color.Black, NodeFont = new Font(tvPermisosDisponibles.Font, FontStyle.Bold) };
            TreeNode nodoPatentes = new TreeNode("PERMISOS SIMPLES (Patentes)") { ForeColor = Color.Black, NodeFont = new Font(tvPermisosDisponibles.Font, FontStyle.Bold) };
            foreach (var comp in listaArbolCompleto)
            {
                TreeNode nodo = new TreeNode(comp.Nombre);
                nodo.Tag = comp;
                if (comp is FamiliaBE)
                {
                    nodo.ForeColor = Color.Blue;
                    nodoRoles.Nodes.Add(nodo);
                }
                else
                {
                    nodo.ForeColor = Color.DarkGreen;
                    nodoPatentes.Nodes.Add(nodo);
                }
            }
            tvPermisosDisponibles.Nodes.Add(nodoRoles);
            tvPermisosDisponibles.Nodes.Add(nodoPatentes);
            tvPermisosDisponibles.ExpandAll();
        }
        private void LlenarTreeView(TreeView tv, List<ComponentePermiso> catalogo)
        {
            tv.Nodes.Clear();
            foreach (var comp in catalogo)
            {
                TreeNode nodo = new TreeNode(comp.Nombre);
                nodo.Tag = comp;
                tv.Nodes.Add(nodo);
                AsignarColorNodo(nodo, comp);
                MostrarRecursivo(nodo, comp);
            }
            tv.ExpandAll();
        }
        private void MostrarRecursivo(TreeNode nodoPadre, ComponentePermiso componente)
        {
            if (componente.ObtenerHijos() == null) return;
            foreach (var hijo in componente.ObtenerHijos())
            {
                TreeNode nodoHijo = new TreeNode(hijo.Nombre);
                nodoHijo.Tag = hijo;
                AsignarColorNodo(nodoHijo, hijo);
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
        private void AsignarColorNodo(TreeNode nodo, ComponentePermiso componente)
        {
            if (componente is FamiliaBE)
            {
                nodo.ForeColor = System.Drawing.Color.Blue;
                nodo.NodeFont = new System.Drawing.Font(tvRoles.Font, System.Drawing.FontStyle.Bold);
            }
            else if (componente is PatenteBE)
            {
                nodo.ForeColor = System.Drawing.Color.DarkGreen;
            }
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
                        NotificarPosibleCambioEnUsuarioActual();
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
                        NotificarPosibleCambioEnUsuarioActual();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cmbUsuarios_SelectedIndexChanged(null, null);
                    }
                }
            }
        }
        private void btnAsignarPatenteARol_Click(object sender, EventArgs e)
        {
            if (tvRoles.SelectedNode == null || tvPermisosDisponibles.SelectedNode == null)
            {
                MessageBox.Show("Debe seleccionar un rol destino en el panel izquierdo y un permiso/rol origen en el panel derecho.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (tvPermisosDisponibles.SelectedNode.Tag == null)
            {
                MessageBox.Show("Por favor, expanda las carpetas y seleccione un permiso individual.", "Acción Denegada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ComponentePermiso destino = (ComponentePermiso)tvRoles.SelectedNode.Tag;
            ComponentePermiso origen = (ComponentePermiso)tvPermisosDisponibles.SelectedNode.Tag;
            if (destino is PatenteBE)
            {
                MessageBox.Show("Prohibido: El nodo destino es un Permiso Simple (Patente). Las patentes no pueden contener otros permisos. Debe seleccionar un Rol (Familia) en color azul.", "Acción Denegada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FamiliaBE familiaDestino = (FamiliaBE)destino;
            try
            {
                familiaDestino.AgregarHijo(origen);
                permisoBLL.GuardarFamiliaCompleta(familiaDestino);
                CargarArbolRoles();
                NotificarPosibleCambioEnUsuarioActual();
                MessageBox.Show($"El permiso/rol '{origen.Nombre}' se asignó correctamente a '{familiaDestino.Nombre}'.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                familiaDestino.RemoverHijo(origen);
                MessageBox.Show(ex.Message, "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnCrearRolAnidado_Click(object sender, EventArgs e)
        {
            if (tvRoles.SelectedNode == null)
            {
                MessageBox.Show("Debe seleccionar un rol padre en el árbol.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ComponentePermiso nodoSeleccionado = (ComponentePermiso)tvRoles.SelectedNode.Tag;
            if (nodoSeleccionado is PatenteBE)
            {
                MessageBox.Show("No puede anidar un rol dentro de un permiso simple.", "Acción Denegada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FamiliaBE familiaPadre = (FamiliaBE)nodoSeleccionado;
            string nombreNuevoRol = Microsoft.VisualBasic.Interaction.InputBox($"Nombre del nuevo sub-rol para '{familiaPadre.Nombre}':", "Nuevo Rol Anidado", "");
            if (!string.IsNullOrWhiteSpace(nombreNuevoRol))
            {
                try
                {
                    permisoBLL.CrearRolDentroDeRol(familiaPadre, nombreNuevoRol);
                    CargarArbolRoles();
                    NotificarPosibleCambioEnUsuarioActual();
                    MessageBox.Show("Sub-rol creado y asignado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void NotificarPosibleCambioEnUsuarioActual()
        {
            if (SessionManager.Instancia.UsuarioActual != null)
            {
                UsuarioBE usuarioLogueado = SessionManager.Instancia.UsuarioActual;
                permisoBLL.LlenarPermisosDeUsuario(usuarioLogueado);
                SessionManager.Instancia.ActualizarUsuarioEnSesion(usuarioLogueado);
            }
        }
    }
}
