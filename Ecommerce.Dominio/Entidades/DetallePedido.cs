using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Dominio.Entidades
{
    public class DetallePedido
    {
        public int Id { get; private set; }
        public int Cantidad { get; private set; }
        public decimal PrecioUnitario { get; private set; }

        // Propiedad calculada (como en tu SQL 'AS')
        public decimal Subtotal => Cantidad * PrecioUnitario;

        // Claves foráneas
        public int PedidoId { get; private set; }
        public int ArticuloSku { get; private set; } // FK al SKU del artículo

        // Propiedades de navegación
        public Pedido Pedido { get; private set; } = null!;
        public Articulo Articulo { get; private set; } = null!;

        public void AgregarArticulo(Articulo articulo, int cantidad)
        {
            Articulo = articulo;
            ArticuloSku = articulo.Sku;
            Cantidad = cantidad;
            PrecioUnitario = articulo.Precio;
        }
    }
}
