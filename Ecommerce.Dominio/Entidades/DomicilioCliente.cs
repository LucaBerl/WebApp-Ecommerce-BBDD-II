using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Dominio.Entidades
{
    public class DomicilioCliente
    {
        public int Id { get; private set; }
        public string Calle { get; private set; }
        public string? Altura { get; private set; }
        public string Ciudad { get; private set; }
        public string Provincia { get; private set; }
        public string CodigoPostal { get; private set; }

        // Clave foránea
        public int ClienteId { get; private set; }

        // Propiedad de navegación
        public Cliente Cliente { get; private set; } = null!;

        // Un domicilio puede usarse en muchos envíos
        public ICollection<Envio> Envios { get; private set; } = new List<Envio>();
    }
}
