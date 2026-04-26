using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Eventos
{
    public class LogInEventArgs : EventArgs
    {
        public UsuarioBE Usuario { get; set; }
        public LogInEventArgs(UsuarioBE usuario)
        {
            Usuario = usuario;
        }
    }
}
