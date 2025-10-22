using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Dominio.Entidades;

namespace Ecommerce.Aplicaciones.Interfaces
{
    internal interface IPedidoRepository
    {
        Task<Pedido?> GetByIdAsync(int id);
        Task<Pedido?> GetCarritoActivo(int IDCliente);


    }
}
