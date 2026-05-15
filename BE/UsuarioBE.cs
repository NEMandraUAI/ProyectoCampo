using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class UsuarioBE
    {
        [DigitoVerificador(1)]
        public int ID { get; set; }
        [DigitoVerificador(2)]
        public string Nombre { get; set; }
        [DigitoVerificador(3)]
        public string Clave { get; set; }
        [DigitoVerificador(4)]
        public int IntentosFallidos { get; set; }
        [DigitoVerificador(5)]
        public bool Bloqueado { get; set; }
        public string DVH { get; set; }
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
