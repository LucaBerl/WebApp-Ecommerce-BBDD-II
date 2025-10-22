using Ecommerce.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infraestructura.Repositorios
{
    internal class PedidoRepository
    {
        private readonly EcommerceDbContext _context;

        // Pedimos el DbContext en el constructor
        public PedidoRepository(EcommerceDbContext context)
        {
            _context = context;
        }

        public async Task<Pedido?> GetByIdAsync(int id)
        {
            return await _context.Pedidos.FindAsync(id);
        }

        public async Task<Pedido?> GetCarritoActivo(int IDCliente)
        {
            return await _context.Pedidos
                .Where(p => p.ClienteId == IDCliente && p.Estado == Dominio.Enumeraciones.EstadoPedido.EnProceso)
                .FirstOrDefaultAsync();
        }
    }
}
