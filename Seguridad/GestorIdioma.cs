using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seguridad
{
    public class GestorIdioma : ISubjectIdioma
    {
        private static GestorIdioma _instancia;
        private static readonly object _candado = new object();
        private List<IObserverIdioma> observadores = new List<IObserverIdioma>();
        public IdiomaBE IdiomaActual { get; private set; }
        private GestorIdioma()
        {
            IdiomaActual = new IdiomaBE { ID = 1, Nombre = "Español" };
        }
        public static GestorIdioma Instancia
        {
            get
            {
                lock (_candado)
                {
                    if (_instancia == null)
                        _instancia = new GestorIdioma();
                    return _instancia;
                }
            }
        }
        public void CambiarIdioma(IdiomaBE nuevoIdioma)
        {
            IdiomaActual = nuevoIdioma;
            Notificar();
        }
        public void Suscribir(IObserverIdioma observador)
        {
            if (!observadores.Contains(observador))
                observadores.Add(observador);
        }
        public void Desuscribir(IObserverIdioma observador)
        {
            if (observadores.Contains(observador))
                observadores.Remove(observador);
        }
        public void Notificar()
        {
            var copiaObservadores = new List<IObserverIdioma>(observadores);
            foreach (var obs in copiaObservadores)
            {
                obs.ActualizarIdioma(IdiomaActual);
            }
        }
    }
}
