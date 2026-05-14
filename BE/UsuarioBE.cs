using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class UsuarioBE
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Clave { get; set; }
        public int IntentosFallidos { get; set; }
        public bool Bloqueado { get; set; }
        public UsuarioMemento CrearMemento()
        {
            return new UsuarioMemento(this.Nombre, this.Clave, this.IntentosFallidos, this.Bloqueado);
        }
        public void RestaurarMemento(UsuarioMemento memento)
        {
            this.Nombre = memento.Nombre;
            this.Clave = memento.Clave;
            this.IntentosFallidos = memento.IntentosFallidos;
            this.Bloqueado = memento.Bloqueado;
        }
    }
}
