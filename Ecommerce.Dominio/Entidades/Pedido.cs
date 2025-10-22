using Ecommerce.Dominio.Enumeraciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Dominio.Entidades
{
    public class Pedido
    {
        public int Id { get; private set; }
        public DateTime FechaCreacion { get; private set; }
        public DateTime? FechaUltMovimiento { get; private set; }
        public EstadoPedido Estado { get; private set; } // Usamos el Enum
        public decimal? MontoTotal { get; private set; }

        // Clave foránea
        public int ClienteId { get; private set; }

        // Propiedades de navegación
        public Cliente Cliente { get; private set; } = null!;

        // Un pedido tiene muchos detalles (líneas de pedido)
        public ICollection<DetallePedido> Detalles { get; private set; } = new List<DetallePedido>();

        // Un pedido tiene un pago (relación 1 a 1)
        public Pago? Pago { get; private set; }

        // Un pedido puede tener uno o más envíos
        public ICollection<Envio> Envios { get; private set; } = new List<Envio>();

        public void AgregarDetalle(DetallePedido detalle)
        {
            Detalles.Add(detalle);
        }
    }
}
