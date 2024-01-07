using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyectoprogreso2.Models
{
    public class IntencionDescripcionDTO
    {
        public int IdIntencionDescripcion { get; set; }
        public int Cantidad { get; set; }
        // Clave foránea para la relación uno a muchos con Cliente
        public int ProductoColorTallaIdProductoColorTalla { get; set; }
        // Clave foránea para la relación uno a muchos con Cliente
        public int IntencionCompraIdIntencionCompra { get; set; }
    }
}
