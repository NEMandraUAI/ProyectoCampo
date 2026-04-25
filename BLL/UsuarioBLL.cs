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
    public class UsuarioBLL
    {
        private UsuarioDAL usuarioDAL = new UsuarioDAL();
        public UsuarioBE IniciarSesion(string usuario, string clavePlana)
        {
            UsuarioBE usuarioBD = usuarioDAL.ObtenerPorUsername(usuario);
            if (usuarioBD == null)
            {
                return null;
            }
            bool hashCoincide = CryptoManager.Comparar(clavePlana, usuarioBD.Clave);
            if (hashCoincide)
            {
                SessionManager.Instancia.Iniciar(usuarioBD);
                return usuarioBD;
            }
            else
            {
                return null;
            }
        }
        public void CerrarSesion()
        {
            SessionManager.Instancia.Cerrar();
        }
    }
}
