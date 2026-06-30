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
    public class IdiomaBLL
    {
        private IdiomaDAL idiomaDAL = new IdiomaDAL();
        public List<IdiomaBE> ObtenerIdiomas() => idiomaDAL.ObtenerIdiomas();
        public Dictionary<string, string> ObtenerTraducciones(IdiomaBE idioma, string formulario)
        {
            if (idioma == null) return new Dictionary<string, string>();
            return idiomaDAL.ObtenerTraduccionesPorFormulario(idioma.ID, formulario);
        }
        public void AgregarNuevoIdioma(string nombre, string sufijo)
        {
            UsuarioBE usuarioActual = SessionManager.Instancia.UsuarioActual;
            if (usuarioActual == null || !usuarioActual.TienePermiso("GESTION_IDIOMAS"))
                throw new Exception("Operación Denegada. No tiene los permisos de Administrador necesarios para crear idiomas.");
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(sufijo))
                throw new Exception("Debe proveer un nombre y un sufijo (Ej: POR, ENG).");
            idiomaDAL.CrearIdiomaConSufijo(nombre, sufijo.ToUpper());
        }
        public void EliminarIdioma(int idIdioma)
        {
            UsuarioBE usuarioActual = SessionManager.Instancia.UsuarioActual;
            if (usuarioActual == null || !usuarioActual.TienePermiso("GESTION_IDIOMAS"))
                throw new Exception("Operación Denegada. No tiene permisos para eliminar idiomas.");
            if (idIdioma == 1)
                throw new Exception("Seguridad: No se puede eliminar el idioma predeterminado del sistema (Español).");
            idiomaDAL.EliminarIdioma(idIdioma);
        }
        public List<TraduccionBE> ObtenerTodasLasTraducciones(int idIdioma)
        {
            return idiomaDAL.ObtenerTodasLasTraducciones(idIdioma);
        }
        public void ActualizarTraducciones(List<TraduccionBE> traducciones)
        {
            UsuarioBE usuarioActual = SessionManager.Instancia.UsuarioActual;
            if (usuarioActual == null || !usuarioActual.TienePermiso("GESTION_IDIOMAS"))
                throw new Exception("Operación Denegada. No tiene permisos para modificar traducciones.");
            idiomaDAL.ActualizarTraducciones(traducciones);
        }
    }
}
