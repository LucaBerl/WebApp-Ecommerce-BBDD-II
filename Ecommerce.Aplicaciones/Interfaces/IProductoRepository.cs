using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Dominio.Entidades;

namespace Ecommerce.Aplicaciones.Interfaces
{
    public interface IProductoRepository
    {
        // Contrato para obtener un producto por su ID
        Task<Producto?> GetByIdAsync(int id);

        // Contrato para obtener todos los productos
        Task<IEnumerable<Producto>> GetAllAsync();

        // Contrato para añadir un nuevo producto
        Task AddAsync(Producto producto);

        // Contrato para actualizar un producto
        void Update(Producto producto); 

        // Contrato para borrar un producto
        void Remove(Producto producto); 
    }
}
