using System;
using System.Collections.Generic;
using System.Text;
using BE;

namespace Seguridad
{
    public class SessionManager
    {
        private static SessionManager _instancia;
        private static readonly object candado = new object();
        public UsuarioBE UsuarioActual { get; private set; }
        public static SessionManager Instancia
        {
            get
            {
                lock (candado)
                {
                    if (_instancia == null)
                    {
                        _instancia = new SessionManager();
                    }
                    return _instancia;
                }
            }
        }
        private SessionManager() { }
        public void Iniciar(UsuarioBE usuario)
        {
            UsuarioActual = usuario;
        }
        public void Cerrar()
        {
            UsuarioActual = null;
        }
    }
}
