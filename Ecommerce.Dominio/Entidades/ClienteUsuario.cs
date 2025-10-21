using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Dominio.Entidades
{
    public class ClienteUsuario
    {
        public int Id { get; private set; }
        public string Email { get; private set; }
        public string ContraseniaHash { get; private set; } // El hash, no la contraseña
        public DateTime FechaAlta { get; private set; }
        public bool Estado { get; private set; }

        // Clave foránea (para la relación 1 a 1)
        public int ClienteId { get; private set; }

        // Propiedad de navegación
        public Cliente Cliente { get; private set; } = null!;
    }
}
