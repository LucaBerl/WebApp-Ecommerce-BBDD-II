using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Dominio.Enumeraciones
{
    
    /// Género de aplicabilidad del producto (M, F, U).
    
    public enum GeneroProducto
    {
        M,
        F,
        U
    }

    
    /// Estado del pedido (EnProceso, Finalizado).
    
    public enum EstadoPedido
    {
        EnProceso,
        Finalizado
    }

    
    /// Estado del envío (Pendiente, EnTransito, Entregado, Cancelado).
    
    public enum EstadoEnvio
    {
        Pendiente,
        EnTransito,
        Entregado,
        Cancelado
    }
}
