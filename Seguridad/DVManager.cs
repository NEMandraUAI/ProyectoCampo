using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seguridad
{
    public class DVManager
    {
        public static string CalcularDVH<T>(T entidad)
        {
            var tipo = typeof(T);
            var propiedades = tipo.GetProperties()
                .Where(p => p.GetCustomAttributes(typeof(DigitoVerificadorAttribute), false).Any())
                .Select(p => new
                {
                    Propiedad = p,
                    Orden = ((DigitoVerificadorAttribute)p.GetCustomAttributes(typeof(DigitoVerificadorAttribute), false).First()).Orden
                })
                .OrderBy(x => x.Orden);
            StringBuilder sb = new StringBuilder();
            foreach (var item in propiedades)
            {
                var valor = item.Propiedad.GetValue(entidad);
                sb.Append(valor != null ? valor.ToString() : "");
            }
            return CryptoManager.GenerarHash(sb.ToString());
        }
        public static string CalcularDVV(List<string> listaDVH)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var dvh in listaDVH)
            {
                sb.Append(dvh);
            }
            return CryptoManager.GenerarHash(sb.ToString());
        }
    }
}
