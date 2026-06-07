using BE;
using DAL;
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
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(sufijo))
                throw new System.Exception("Debe proveer un nombre y un sufijo (Ej: POR, ENG).");
            idiomaDAL.CrearIdiomaConSufijo(nombre, sufijo.ToUpper());
        }
    }
}
