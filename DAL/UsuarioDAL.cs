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
            SqlConnection cn = ConexionDAL.Instancia.ObtenerConexion();
            string consulta = "SELECT ID, Nombre, Clave, IntentosFallidos, Bloqueado FROM Usuario WHERE Nombre = @Nombre";
            SqlCommand cmd = new SqlCommand(consulta, cn);
            cmd.Parameters.AddWithValue("@Nombre", nombreUsuario);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                usuario = new UsuarioBE();
                usuario.ID = Convert.ToInt32(reader["ID"]);
                usuario.Nombre = reader["Nombre"].ToString();
                usuario.Clave = reader["Clave"].ToString();
                usuario.IntentosFallidos = Convert.ToInt32(reader["IntentosFallidos"]);
                usuario.Bloqueado = Convert.ToBoolean(reader["Bloqueado"]);
            }
            reader.Close();
            ConexionDAL.Instancia.CerrarConexion();
            return usuario;
        }
        public void ActualizarEstadoUsuario(UsuarioBE usuario)
        {
            SqlConnection cn = ConexionDAL.Instancia.ObtenerConexion();
            string consulta = "UPDATE Usuario SET IntentosFallidos = @Intentos, Bloqueado = @Bloqueado WHERE ID = @ID";
            SqlCommand cmd = new SqlCommand(consulta, cn);
            cmd.Parameters.AddWithValue("@Intentos", usuario.IntentosFallidos);
            cmd.Parameters.AddWithValue("@Bloqueado", usuario.Bloqueado);
            cmd.Parameters.AddWithValue("@ID", usuario.ID);
            cmd.ExecuteNonQuery();
            ConexionDAL.Instancia.CerrarConexion();
        }
    }
}
