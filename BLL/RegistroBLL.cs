using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class RegistroBLL
    {
        private RegistroDAL registroDAL = new RegistroDAL();
        public void RegistrarEvento(string evento, UsuarioBE pUsuario)
        {
            RegistroBE registro = new RegistroBE();
            registro.FechaHora = DateTime.Now;
            registro.Evento = evento;
            registro.ID_Usuario = pUsuario.ID;
            registroDAL.GuardarRegistro(registro, pUsuario);
        }
        public List<RegistroBE> ConsultarLogs() => registroDAL.ConsultaLogs();
    }
}
