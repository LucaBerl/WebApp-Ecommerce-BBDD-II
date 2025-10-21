using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Dominio.Entidades
{
    public class Categoria
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }
        public string? Descripcion { get; private set; }

        // Propiedad de navegación: Una categoría tiene muchos productos
        public ICollection<Producto> Productos { get; private set; } = new List<Producto>();
    }
}
