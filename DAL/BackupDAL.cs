using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BackupDAL
    {
        public void RealizarBackup(string rutaArchivo)
        {
            using (SqlConnection cn = ConexionDAL.Instancia.ObtenerConexion())
            {
                cn.Open();
                string query = $"BACKUP DATABASE [ProyectoCampo] TO DISK = '{rutaArchivo}' WITH FORMAT, MEDIANAME = 'SQLServerBackups', NAME = 'Full Backup of ProyectoCampo';";
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void RestaurarBackup(string rutaArchivo)
        {
            string cadenaOriginal = ConexionDAL.Instancia.ObtenerConexion().ConnectionString;
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(cadenaOriginal);
            builder.InitialCatalog = "master";
            using (SqlConnection cn = new SqlConnection(builder.ConnectionString))
            {
                cn.Open();
                string querySingle = "ALTER DATABASE [ProyectoCampo] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;";
                using (SqlCommand cmd = new SqlCommand(querySingle, cn))
                {
                    cmd.ExecuteNonQuery();
                }
                string queryRestore = $"RESTORE DATABASE [ProyectoCampo] FROM DISK = '{rutaArchivo}' WITH REPLACE;";
                using (SqlCommand cmd = new SqlCommand(queryRestore, cn))
                {
                    cmd.ExecuteNonQuery();
                }
                string queryMulti = "ALTER DATABASE [ProyectoCampo] SET MULTI_USER;";
                using (SqlCommand cmd = new SqlCommand(queryMulti, cn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
