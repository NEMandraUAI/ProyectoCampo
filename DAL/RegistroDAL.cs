using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public class RegistroDAL
    {
        public void GuardarRegistro(RegistroBE pRegistro, UsuarioBE pUsuario)
        {
            SqlConnection cn = ConexionDAL.Instancia.ObtenerConexion();
            string consulta = "INSERT INTO Bitacora (ID_Usuario, FechaHora, Evento, Criticidad) VALUES (@ID_Usuario, @FechaHora, @Evento, @Criticidad)";
            SqlCommand cmd = new SqlCommand(consulta, cn);
            cmd.Parameters.AddWithValue("@ID_Usuario", pUsuario.ID);
            cmd.Parameters.AddWithValue("@FechaHora", pRegistro.FechaHora);
            cmd.Parameters.AddWithValue("@Evento", pRegistro.Evento);
            cmd.Parameters.AddWithValue("@Criticidad", pRegistro.Criticidad);
            cmd.ExecuteNonQuery();
            ConexionDAL.Instancia.CerrarConexion();
        }
        public List<RegistroBE> ConsultaLogsFiltros(DateTime desde, DateTime hasta, string criticidad, int? idUsuario, string palabraClave)
        {
            List<RegistroBE> lr = new List<RegistroBE>();
            SqlConnection cn = ConexionDAL.Instancia.ObtenerConexion();
            StringBuilder consulta = new StringBuilder(@"
                SELECT b.Id_Reg, b.Id_Usuario, b.FechaHora, b.Evento, b.Criticidad, u.Nombre as NombreUsuario 
                FROM Bitacora b 
                INNER JOIN Usuario u ON b.ID_Usuario = u.ID 
                WHERE 1=1");
            SqlCommand cmd = new SqlCommand();
            consulta.Append(" AND b.FechaHora >= @Desde AND b.FechaHora <= @Hasta");
            cmd.Parameters.AddWithValue("@Desde", desde.Date);
            cmd.Parameters.AddWithValue("@Hasta", hasta.Date.AddDays(1).AddSeconds(-1));
            if (!string.IsNullOrEmpty(criticidad) && criticidad != "Todos")
            {
                consulta.Append(" AND b.Criticidad = @Criticidad");
                cmd.Parameters.AddWithValue("@Criticidad", criticidad);
            }
            if (idUsuario.HasValue && idUsuario.Value > 0)
            {
                consulta.Append(" AND b.ID_Usuario = @IdUsuario");
                cmd.Parameters.AddWithValue("@IdUsuario", idUsuario.Value);
            }
            if (!string.IsNullOrWhiteSpace(palabraClave))
            {
                consulta.Append(" AND b.Evento LIKE @Palabra");
                cmd.Parameters.AddWithValue("@Palabra", "%" + palabraClave + "%");
            }
            consulta.Append(" ORDER BY b.FechaHora DESC");
            cmd.CommandText = consulta.ToString();
            cmd.Connection = cn;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                RegistroBE registro = new RegistroBE();
                registro.RegID = Convert.ToInt32(reader["Id_Reg"]);
                registro.ID_Usuario = Convert.ToInt32(reader["Id_Usuario"]);
                registro.FechaHora = Convert.ToDateTime(reader["FechaHora"]);
                registro.Evento = reader["Evento"].ToString();
                registro.Criticidad = reader["Criticidad"].ToString();
                registro.NombreUsuario = reader["NombreUsuario"].ToString();
                lr.Add(registro);
            }
            reader.Close();
            ConexionDAL.Instancia.CerrarConexion();
            return lr;
        }
    }
}
