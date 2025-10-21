using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Dominio.Entidades
{
    public class Pago
    {
        public int Id { get; private set; }
        public string? MetodoPago { get; private set; }
        public DateTime FechaPago { get; private set; }
        public decimal Monto { get; private set; }

        // Clave foránea (relación 1 a 1 con Pedido)
        public int PedidoId { get; private set; }

        // Propiedad de navegación
        public Pedido Pedido { get; private set; } = null!;
    }
}
