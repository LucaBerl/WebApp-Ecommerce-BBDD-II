using Ecommerce.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infraestructura.Repositorios
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly EcommerceDbContext _context;

        // Pedimos el DbContext en el constructor
        public ProductoRepository(EcommerceDbContext context)
        {
            _context = context;
        }

        // --- LECTURA (SELECT) ---

        public async Task<Producto?> GetByIdAsync(int id)
        {
            // LINQ: Traduce esto a: SELECT TOP(1) * FROM PRODUCTO WHERE IDProducto = @id
            return await _context.Productos.FindAsync(id);
        }

        public async Task<IEnumerable<Producto>> GetAllAsync()
        {
            // LINQ: Traduce esto a: SELECT * FROM PRODUCTO
            // .ToListAsync() ejecuta la consulta
            return await _context.Productos.ToListAsync();
        }

        // --- ESCRITURA (INSERT, UPDATE, DELETE) ---

        public async Task AddAsync(Producto producto)
        {
            // LINQ: Esto prepara un: INSERT INTO PRODUCTO (...) VALUES (...)
            await _context.Productos.AddAsync(producto);
            // ¡OJO! Aún no se guarda en la BD. Ver más abajo.
        }

        public void Update(Producto producto)
        {
            // LINQ: Esto le dice a EF Core "este objeto ha cambiado"
            // Preparará un: UPDATE PRODUCTO SET ... WHERE IDProducto = @id
            _context.Productos.Update(producto);
        }

        public void Remove(Producto producto)
        {
            // LINQ: Esto prepara un: DELETE FROM PRODUCTO WHERE IDProducto = @id
            _context.Productos.Remove(producto);
        }
    }
}
