using BE;
using DAL;
using Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BackupBLL
    {
        private BackupDAL backupDAL = new BackupDAL();
        private RegistroBLL registroBLL = new RegistroBLL();
        public void RealizarBackup(string rutaArchivo)
        {
            UsuarioBE usuario = SessionManager.Instancia.UsuarioActual;
            bool tienePermiso = usuario != null && usuario.TienePermiso("REALIZAR_BACKUP");
            if (!tienePermiso)
            {
                throw new Exception("Operación de Seguridad Denegada. Su cuenta no posee los privilegios necesarios (REALIZAR_BACKUP) para generar copias del sistema.");
            }
            backupDAL.RealizarBackup(rutaArchivo);
            registroBLL.RegistrarEvento($"Backup del sistema generado exitosamente en ruta local.", usuario, "ALTA");
        }
        public void RestaurarBackup(string rutaArchivo)
        {
            backupDAL.RestaurarBackup(rutaArchivo);
        }
    }
}
