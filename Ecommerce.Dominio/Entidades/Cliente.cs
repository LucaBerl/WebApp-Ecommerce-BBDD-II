using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Dominio.Entidades
{
    public class Cliente
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }
        public string Apellido { get; private set; }
        public string? Telefono { get; private set; }

        // Propiedades de navegación
        // Un cliente tiene una cuenta de usuario (relación 1 a 1)
        public ClienteUsuario? Usuario { get; private set; }

        // Un cliente puede tener muchos domicilios
        public ICollection<DomicilioCliente> Domicilios { get; private set; } = new List<DomicilioCliente>();

        // Un cliente puede tener muchos pedidos
        public ICollection<Pedido> Pedidos { get; private set; } = new List<Pedido>();
    }
}
