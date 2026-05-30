using BE;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PermisoDAL
    {
        public List<ComponentePermiso> ObtenerComponentesBases()
        {
            List<ComponentePermiso> lista = new List<ComponentePermiso>();
            using (SqlConnection cn = ConexionDAL.Instancia.ObtenerConexion())
            {
                cn.Open();
                string query = "SELECT ID, Nombre, PermisoCodigo, EsFamilia FROM Permiso";
                using (SqlCommand cmd = new SqlCommand(query, cn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        bool esFamilia = Convert.ToBoolean(reader["EsFamilia"]);
                        ComponentePermiso c;
                        if (esFamilia)
                            c = new FamiliaBE();
                        else
                            c = new PatenteBE();

                        c.ID = Convert.ToInt32(reader["ID"]);
                        c.Nombre = reader["Nombre"].ToString();
                        c.PermisoCodigo = reader["PermisoCodigo"] != DBNull.Value ? reader["PermisoCodigo"].ToString() : string.Empty;
                        lista.Add(c);
                    }
                }
            }
            return lista;
        }
        public void LlenarFamiliaComponentes(FamiliaBE familia, List<ComponentePermiso> catalogoBases)
        {
            familia.VaciarHijos();
            using (SqlConnection cn = ConexionDAL.Instancia.ObtenerConexion())
            {
                cn.Open();
                string query = "SELECT ID_Hijo FROM Permiso_Permiso WHERE ID_Padre = @ID";
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@ID", familia.ID);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idHijo = Convert.ToInt32(reader["ID_Hijo"]);
                            var hijo = catalogoBases.FirstOrDefault(c => c.ID == idHijo);
                            if (hijo != null)
                            {
                                familia.AgregarHijo(hijo);
                                if (hijo is FamiliaBE familiaHija)
                                {
                                    LlenarFamiliaComponentes(familiaHija, catalogoBases);
                                }
                            }
                        }
                    }
                }
            }
        }
        public int GuardarFamilia(FamiliaBE familia)
        {
            using (SqlConnection cn = ConexionDAL.Instancia.ObtenerConexion())
            {
                cn.Open();
                string query = "INSERT INTO Permiso (Nombre, EsFamilia) VALUES (@Nombre, 1); SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@Nombre", familia.Nombre);
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }
        public void EliminarFamilia(int idFamilia)
        {
            using (SqlConnection cn = ConexionDAL.Instancia.ObtenerConexion())
            {
                cn.Open();
                using (SqlCommand cmd1 = new SqlCommand("DELETE FROM Permiso_Permiso WHERE ID_Padre = @ID OR ID_Hijo = @ID", cn))
                {
                    cmd1.Parameters.AddWithValue("@ID", idFamilia);
                    cmd1.ExecuteNonQuery();
                }
                using (SqlCommand cmd2 = new SqlCommand("DELETE FROM Permiso WHERE ID = @ID", cn))
                {
                    cmd2.Parameters.AddWithValue("@ID", idFamilia);
                    cmd2.ExecuteNonQuery();
                }
            }
        }
        public void GuardarRelacionFamiliaPermiso(int idPadre, int idHijo)
        {
            using (SqlConnection cn = ConexionDAL.Instancia.ObtenerConexion())
            {
                cn.Open();
                string query = "INSERT INTO Permiso_Permiso (ID_Padre, ID_Hijo) VALUES (@Padre, @Hijo)";
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@Padre", idPadre);
                    cmd.Parameters.AddWithValue("@Hijo", idHijo);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void BorrarRelacionesFamilia(int idPadre)
        {
            using (SqlConnection cn = ConexionDAL.Instancia.ObtenerConexion())
            {
                cn.Open();
                string query = "DELETE FROM Permiso_Permiso WHERE ID_Padre = @ID";
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@ID", idPadre);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void AsignarPermisoUsuario(int idUsuario, int idPermiso)
        {
            using (SqlConnection cn = ConexionDAL.Instancia.ObtenerConexion())
            {
                cn.Open();
                string query = "INSERT INTO Usuario_Permiso (ID_Usuario, ID_Permiso) VALUES (@Usu, @Per)";
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@Usu", idUsuario);
                    cmd.Parameters.AddWithValue("@Per", idPermiso);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void LimpiarPermisosUsuario(int idUsuario)
        {
            using (SqlConnection cn = ConexionDAL.Instancia.ObtenerConexion())
            {
                cn.Open();
                string query = "DELETE FROM Usuario_Permiso WHERE ID_Usuario = @Usu";
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@Usu", idUsuario);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public bool RolAsignadoAAlgunUsuario(int idPermiso)
        {
            using (SqlConnection cn = ConexionDAL.Instancia.ObtenerConexion())
            {
                cn.Open();
                string query = "SELECT COUNT(*) FROM Usuario_Permiso WHERE ID_Permiso = @ID";
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@ID", idPermiso);
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
        }
        public List<ComponentePermiso> ObtenerPermisosDeUsuario(int idUsuario, List<ComponentePermiso> catalogoBases)
        {
            List<ComponentePermiso> asignados = new List<ComponentePermiso>();
            using (SqlConnection cn = ConexionDAL.Instancia.ObtenerConexion())
            {
                cn.Open();
                string query = "SELECT ID_Permiso FROM Usuario_Permiso WHERE ID_Usuario = @Usu";
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@Usu", idUsuario);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idPermiso = Convert.ToInt32(reader["ID_Permiso"]);
                            var comp = catalogoBases.FirstOrDefault(c => c.ID == idPermiso);
                            if (comp != null) asignados.Add(comp);
                        }
                    }
                }
            }
            return asignados;
        }
        public int CantidadUsuariosConRol(int idRol)
        {
            using (SqlConnection cn = ConexionDAL.Instancia.ObtenerConexion())
            {
                cn.Open();
                string query = "SELECT COUNT(*) FROM Usuario_Permiso WHERE ID_Permiso = @ID";
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@ID", idRol);
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }
    }
}
