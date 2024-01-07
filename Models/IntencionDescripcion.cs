using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyectoprogreso2.Models
{
    public class IntencionDescripcion
    {
        public int IdIntencionDescripcion { get; set; }

        public int Cantidad { get; set; }
        public double PrecioTotal { get; set; }
        public bool Status { get; set; }
        // Clave foránea para la relación uno a muchos con Cliente

        public ProductoColorTalla ProductoColorTalla { get; set; }
        // Clave foránea para la relación uno a muchos con Cliente

        public IntencionCompra IntencionCompra { get; set; }
    }
}
