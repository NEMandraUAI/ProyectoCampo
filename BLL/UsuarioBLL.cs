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
        private RegistroBLL registroBLL = new RegistroBLL();
        public UsuarioBE IniciarSesion(string usuario, string clavePlana)
        {
            if (SessionManager.Instancia.UsuarioActual != null)
            {
                throw new Exception("Operación denegada. Ya existe una sesión activa en el sistema.");
            }
            UsuarioBE usuarioBD = usuarioDAL.ObtenerPorUsername(usuario);
            if (usuarioBD == null)
            {
                throw new Exception("Usuario o clave incorrectos.");
            }
            if (usuarioBD.Bloqueado)
            {
                registroBLL.RegistrarEvento("Intento de acceso a cuenta bloqueada: " + usuarioBD.Nombre, usuarioBD, "CRÍTICO");
                throw new Exception("El usuario se encuentra bloqueado por múltiples intentos fallidos. Contacte al administrador.");
            }
            bool hashCoincide = CryptoManager.Comparar(clavePlana, usuarioBD.Clave);
            if (hashCoincide)
            {
                if (usuarioBD.IntentosFallidos > 0)
                {
                    usuarioBD.IntentosFallidos = 0;
                    usuarioDAL.ActualizarEstadoUsuario(usuarioBD);
                }
                registroBLL.RegistrarEvento("Inicio de sesión del usuario: " + usuarioBD.Nombre, usuarioBD);
                SessionManager.Instancia.Iniciar(usuarioBD);
                return usuarioBD;
            }
            else
            {
                usuarioBD.IntentosFallidos++;
                if (usuarioBD.IntentosFallidos >= 3)
                {
                    usuarioBD.Bloqueado = true;
                    usuarioDAL.ActualizarEstadoUsuario(usuarioBD);
                    registroBLL.RegistrarEvento($"Usuario <{usuarioBD.Nombre}> bloqueado por superar límite de intentos (3)", usuarioBD, "CRÍTICO");
                    throw new Exception("Su cuenta ha sido bloqueada tras 3 intentos fallidos.");
                }
                else
                {
                    usuarioDAL.ActualizarEstadoUsuario(usuarioBD);
                    registroBLL.RegistrarEvento($"Intento de inicio de sesión fallido. Intento #{usuarioBD.IntentosFallidos}. Usuario: " + usuarioBD.Nombre, usuarioBD, "ALERTA");
                    int intentosRestantes = 3 - usuarioBD.IntentosFallidos;
                    throw new Exception($"Usuario o clave incorrectos. Le quedan {intentosRestantes} intento(s).");
                }
            }
        }
        public void CerrarSesion()
        {
            if (SessionManager.Instancia.UsuarioActual != null)
            {
                registroBLL.RegistrarEvento("Cierre de sesión del usuario: " + SessionManager.Instancia.UsuarioActual.Nombre, SessionManager.Instancia.UsuarioActual);
            }
            SessionManager.Instancia.Cerrar();
        }
        public List<UsuarioBE> ListarTodos() => usuarioDAL.ListarTodos();
        public void RegistrarUsuario(string nombre, string clavePlana)
        {
            if (usuarioDAL.ObtenerPorUsername(nombre) != null)
            {
                throw new Exception("El nombre de usuario ya se encuentra registrado.");
            }
            UsuarioBE nuevoUsuario = new UsuarioBE();
            nuevoUsuario.Nombre = nombre;
            nuevoUsuario.Clave = CryptoManager.GenerarHash(clavePlana);
            usuarioDAL.CrearUsuario(nuevoUsuario);
            UsuarioBE usuarioCreado = usuarioDAL.ObtenerPorUsername(nombre);
            registroBLL.RegistrarEvento("Nuevo usuario registrado: " + nombre, usuarioCreado, "ALTA");
        }
    }
}
