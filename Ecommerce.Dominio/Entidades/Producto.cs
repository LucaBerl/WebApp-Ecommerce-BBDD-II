using Ecommerce.Dominio.Enumeraciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Dominio.Entidades
{
    public class Producto
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }
        public string? Descripcion { get; private set; }
        public GeneroProducto Sexo { get; private set; } // Usamos el Enum
        public bool Estado { get; private set; }

        // Claves foráneas
        public int CategoriaId { get; private set; }
        public int MarcaId { get; private set; }

        // Propiedades de navegación (los objetos relacionados)
        public Categoria Categoria { get; private set; } = null!; // null! indica a C# que confiamos en que EF Core lo cargará
        public Marca Marca { get; private set; } = null!;

        // Un producto se compone de muchos artículos (variantes de talle/color)
        public ICollection<Articulo> Articulos { get; private set; } = new List<Articulo>();
    }
}
