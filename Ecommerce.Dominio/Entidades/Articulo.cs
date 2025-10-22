using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Dominio.Entidades
{
    public class Articulo // La variante específica (SKU)
    {
        public int Sku { get; private set; } // PK de la tabla ARTICULO
        public string Color { get; private set; } = null!;
        public string Talle { get; private set; } = null!;
        public int CantidadStock { get; private set; }
        public decimal Precio { get; private set; }

        // Clave foránea
        public int ProductoId { get; private set; }

        // Propiedades de navegación
        public Producto Producto { get; private set; } = null!;
        public ICollection<ImagenArticulo> Imagenes { get; private set; } = new List<ImagenArticulo>();

        // Un artículo puede estar en muchos detalles de pedido
        public ICollection<DetallePedido> DetallesPedido { get; private set; } = new List<DetallePedido>();
    }
}
