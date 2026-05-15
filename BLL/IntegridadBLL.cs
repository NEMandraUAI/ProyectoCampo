using DAL;
using Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class IntegridadBLL
    {
        private IntegridadDAL integridadDAL = new IntegridadDAL();
        public void VerificarIntegridadSistema()
        {
            var usuarios = integridadDAL.LeerTodosLosUsuariosCrudo();
            List<string> dvhCalculados = new List<string>();
            foreach (var usu in usuarios)
            {
                string dvhReal = DVManager.CalcularDVH(usu);
                if (dvhReal != usu.DVH)
                {
                    throw new Exception($"Error de Integridad: El registro del usuario con ID {usu.ID} ha sido alterado externamente.");
                }
                dvhCalculados.Add(dvhReal);
            }
            string dvvCalculado = DVManager.CalcularDVV(dvhCalculados);
            string dvvBaseDatos = integridadDAL.ObtenerDVV("Usuario");
            if (dvvBaseDatos != null && dvvCalculado != dvvBaseDatos)
            {
                throw new Exception("Error de Integridad: Se han insertado, eliminado o reordenado registros en la tabla Usuario de forma externa.");
            }
        }
        public void ActualizarDVVGeneral()
        {
            var usuarios = integridadDAL.LeerTodosLosUsuariosCrudo();
            var dvhs = usuarios.Select(u => u.DVH).ToList();
            string nuevoDVV = DVManager.CalcularDVV(dvhs);
            integridadDAL.ActualizarDVV("Usuario", nuevoDVV);
        }
        public void ForzarRecalculoDeTodaLaBase()
        {
            var usuarios = integridadDAL.LeerTodosLosUsuariosCrudo();
            List<string> dvhsCalculados = new List<string>();
            DAL.UsuarioDAL usuarioDAL = new DAL.UsuarioDAL();
            foreach (var usu in usuarios)
            {
                string dvhCorrecto = Seguridad.DVManager.CalcularDVH(usu);
                usu.DVH = dvhCorrecto;
                usuarioDAL.ActualizarDVH(usu.ID, dvhCorrecto);
                dvhsCalculados.Add(dvhCorrecto);
            }
            string dvvCorrecto = Seguridad.DVManager.CalcularDVV(dvhsCalculados);
            integridadDAL.ActualizarDVV("Usuario", dvvCorrecto);
        }
    }
}
