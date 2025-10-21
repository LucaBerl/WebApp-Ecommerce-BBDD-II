using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Dominio.Entidades
{
    public class ImagenArticulo
    {
        public int Id { get; private set; }
        public string Url { get; private set; }

        // Clave foránea al SKU del artículo
        public int ArticuloSku { get; private set; }

        // Propiedad de navegación
        public Articulo Articulo { get; private set; } = null!;
    }
}
