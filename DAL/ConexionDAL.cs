using Microsoft.Data.SqlClient;
using System.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public sealed class ConexionDAL
    {
        private static ConexionDAL instancia;
        private static readonly object candado = new object();
        private string cadenaConexion;
        private ConexionDAL()
        {
            cadenaConexion = ConfigurationManager.ConnectionStrings["CadenaProyectoCampo"].ConnectionString;
        }
        public static ConexionDAL Instancia
        {
            get
            {
                lock (candado)
                {
                    if (instancia == null)
                    {
                        instancia = new ConexionDAL();
                    }
                    return instancia;
                }
            }
        }
        public SqlConnection ObtenerConexion()
        {
            return new SqlConnection(cadenaConexion);
        }
    }
}
