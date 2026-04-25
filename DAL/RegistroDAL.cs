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
            string consulta = "INSERT INTO Bitacora (ID_Usuario, FechaHora, Evento) VALUES (@ID_Usuario, @FechaHora, @Evento)";
            SqlCommand cmd = new SqlCommand(consulta, cn);
            cmd.Parameters.AddWithValue("@ID_Usuario", pUsuario.ID);
            cmd.Parameters.AddWithValue("@FechaHora", pRegistro.FechaHora);
            cmd.Parameters.AddWithValue("@Evento", pRegistro.Evento);
            cmd.ExecuteNonQuery();
            ConexionDAL.Instancia.CerrarConexion();
        }
        public List<RegistroBE> ConsultaLogs()
        {
            List<RegistroBE> lr = new List<RegistroBE>();
            RegistroBE registro = null;
            SqlConnection cn = ConexionDAL.Instancia.ObtenerConexion();
            string consulta = "SELECT * FROM Bitacora";
            SqlCommand cmd = new SqlCommand(consulta, cn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                registro = new RegistroBE();
                registro.RegID = Convert.ToInt32(reader["Id_Reg"]);
                registro.ID_Usuario = Convert.ToInt32(reader["Id_Usuario"]);
                registro.FechaHora = Convert.ToDateTime(reader["FechaHora"]);
                registro.Evento = reader["Evento"].ToString();
                lr.Add(registro);
            }
            reader.Close();
            ConexionDAL.Instancia.CerrarConexion();
            return lr;
        }
    }
}
