using Ecommerce.Dominio.Enumeraciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Dominio.Entidades
{
    public class Envio
    {
        public int Id { get; private set; }
        public EstadoEnvio Estado { get; private set; } // Usamos el Enum
        public DateTime FechaUltMovimiento { get; private set; }
        public string? Tracking { get; private set; }

        // Claves foráneas
        public int PedidoId { get; private set; }
        public int DomicilioId { get; private set; }

        // Propiedades de navegación
        public Pedido Pedido { get; private set; } = null!;
        public DomicilioCliente Domicilio { get; private set; } = null!;
    }
}
