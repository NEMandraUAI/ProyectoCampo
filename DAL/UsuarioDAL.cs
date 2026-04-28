using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public class UsuarioDAL
    {
        public UsuarioBE ObtenerPorUsername(string nombreUsuario)
        {
            UsuarioBE usuario = null;
            using (SqlConnection cn = ConexionDAL.Instancia.ObtenerConexion())
            {
                cn.Open();
                string consulta = "SELECT ID, Nombre, Clave, IntentosFallidos, Bloqueado FROM Usuario WHERE Nombre = @Nombre";
                using (SqlCommand cmd = new SqlCommand(consulta, cn))
                {
                    cmd.Parameters.AddWithValue("@Nombre", nombreUsuario);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            usuario = new UsuarioBE();
                            usuario.ID = Convert.ToInt32(reader["ID"]);
                            usuario.Nombre = reader["Nombre"].ToString();
                            usuario.Clave = reader["Clave"].ToString();
                            usuario.IntentosFallidos = Convert.ToInt32(reader["IntentosFallidos"]);
                            usuario.Bloqueado = Convert.ToBoolean(reader["Bloqueado"]);
                        }
                    }
                }
            }
            return usuario;
        }
        public void ActualizarEstadoUsuario(UsuarioBE usuario)
        {
            using (SqlConnection cn = ConexionDAL.Instancia.ObtenerConexion())
            {
                cn.Open();
                string consulta = "UPDATE Usuario SET IntentosFallidos = @Intentos, Bloqueado = @Bloqueado WHERE ID = @ID";
                using (SqlCommand cmd = new SqlCommand(consulta, cn))
                {
                    cmd.Parameters.AddWithValue("@Intentos", usuario.IntentosFallidos);
                    cmd.Parameters.AddWithValue("@Bloqueado", usuario.Bloqueado);
                    cmd.Parameters.AddWithValue("@ID", usuario.ID);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public List<UsuarioBE> ListarTodos()
        {
            List<UsuarioBE> lista = new List<UsuarioBE>();
            using (SqlConnection cn = ConexionDAL.Instancia.ObtenerConexion())
            {
                cn.Open();
                string consulta = "SELECT ID, Nombre FROM Usuario";
                using (SqlCommand cmd = new SqlCommand(consulta, cn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UsuarioBE usuario = new UsuarioBE();
                            usuario.ID = Convert.ToInt32(reader["ID"]);
                            usuario.Nombre = reader["Nombre"].ToString();
                            lista.Add(usuario);
                        }
                    }
                }
            }
            return lista;
        }
        public void CrearUsuario(UsuarioBE nuevoUsuario)
        {
            using (SqlConnection cn = ConexionDAL.Instancia.ObtenerConexion())
            {
                cn.Open();
                string consulta = "INSERT INTO Usuario (Nombre, Clave, IntentosFallidos, Bloqueado) VALUES (@Nombre, @Clave, 0, 0)";
                using (SqlCommand cmd = new SqlCommand(consulta, cn))
                {
                    cmd.Parameters.AddWithValue("@Nombre", nuevoUsuario.Nombre);
                    cmd.Parameters.AddWithValue("@Clave", nuevoUsuario.Clave);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
