using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Dominio.Entidades
{
    public class Marca
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }

        // Propiedad de navegación: Una marca tiene muchos productos
        public ICollection<Producto> Productos { get; private set; } = new List<Producto>();
    }
}
