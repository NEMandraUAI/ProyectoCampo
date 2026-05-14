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
        public UsuarioBE ObtenerPorID(int idUsuario)
        {
            UsuarioBE usuario = null;
            using (SqlConnection cn = ConexionDAL.Instancia.ObtenerConexion())
            {
                cn.Open();
                string consulta = "SELECT ID, Nombre, Clave, IntentosFallidos, Bloqueado FROM Usuario WHERE ID = @ID";
                using (SqlCommand cmd = new SqlCommand(consulta, cn))
                {
                    cmd.Parameters.AddWithValue("@ID", idUsuario);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            usuario = new UsuarioBE
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                Nombre = reader["Nombre"].ToString(),
                                Clave = reader["Clave"].ToString(),
                                IntentosFallidos = Convert.ToInt32(reader["IntentosFallidos"]),
                                Bloqueado = Convert.ToBoolean(reader["Bloqueado"])
                            };
                        }
                    }
                }
            }
            return usuario;
        }
        public void GuardarEstadoHistorico(UsuarioHistoricoBE historico)
        {
            using (SqlConnection cn = ConexionDAL.Instancia.ObtenerConexion())
            {
                cn.Open();
                string consulta = @"INSERT INTO Usuario_Historico 
                                    (ID_Usuario, Nombre, Clave, IntentosFallidos, Bloqueado, ID_Usuario_Autor, FechaHora, Accion) 
                                    VALUES (@ID_Usu, @Nombre, @Clave, @Intentos, @Bloqueado, @ID_Autor, @Fecha, @Accion)";
                using (SqlCommand cmd = new SqlCommand(consulta, cn))
                {
                    cmd.Parameters.AddWithValue("@ID_Usu", historico.ID);
                    cmd.Parameters.AddWithValue("@Nombre", historico.Nombre);
                    cmd.Parameters.AddWithValue("@Clave", historico.Clave);
                    cmd.Parameters.AddWithValue("@Intentos", historico.IntentosFallidos);
                    cmd.Parameters.AddWithValue("@Bloqueado", historico.Bloqueado);
                    cmd.Parameters.AddWithValue("@ID_Autor", historico.ID_Usuario_Autor);
                    cmd.Parameters.AddWithValue("@Fecha", historico.FechaHora);
                    cmd.Parameters.AddWithValue("@Accion", historico.Accion);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public List<UsuarioHistoricoBE> ListarHistorial(int idUsuario)
        {
            List<UsuarioHistoricoBE> lista = new List<UsuarioHistoricoBE>();
            using (SqlConnection cn = ConexionDAL.Instancia.ObtenerConexion())
            {
                cn.Open();
                string consulta = @"SELECT h.*, u.Nombre AS NombreAutor 
                                    FROM Usuario_Historico h
                                    INNER JOIN Usuario u ON h.ID_Usuario_Autor = u.ID
                                    WHERE h.ID_Usuario = @ID_Usuario ORDER BY h.FechaHora DESC";
                using (SqlCommand cmd = new SqlCommand(consulta, cn))
                {
                    cmd.Parameters.AddWithValue("@ID_Usuario", idUsuario);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new UsuarioHistoricoBE
                            {
                                ID_Cambio = Convert.ToInt32(reader["ID_Cambio"]),
                                ID = Convert.ToInt32(reader["ID_Usuario"]),
                                Nombre = reader["Nombre"].ToString(),
                                Clave = reader["Clave"].ToString(),
                                IntentosFallidos = Convert.ToInt32(reader["IntentosFallidos"]),
                                Bloqueado = Convert.ToBoolean(reader["Bloqueado"]),
                                ID_Usuario_Autor = Convert.ToInt32(reader["ID_Usuario_Autor"]),
                                NombreUsuarioAutor = reader["NombreAutor"].ToString(),
                                FechaHora = Convert.ToDateTime(reader["FechaHora"]),
                                Accion = reader["Accion"].ToString()
                            });
                        }
                    }
                }
            }
            return lista;
        }
        public void ActualizarUsuarioCompleto(UsuarioBE usuario)
        {
            using (SqlConnection cn = ConexionDAL.Instancia.ObtenerConexion())
            {
                cn.Open();
                string consulta = @"UPDATE Usuario 
                                    SET Nombre = @Nombre, 
                                    Clave = @Clave, 
                                    IntentosFallidos = @Intentos, 
                                    Bloqueado = @Bloqueado 
                                    WHERE ID = @ID";
                using (SqlCommand cmd = new SqlCommand(consulta, cn))
                {
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@Clave", usuario.Clave);
                    cmd.Parameters.AddWithValue("@Intentos", usuario.IntentosFallidos);
                    cmd.Parameters.AddWithValue("@Bloqueado", usuario.Bloqueado);
                    cmd.Parameters.AddWithValue("@ID", usuario.ID);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
