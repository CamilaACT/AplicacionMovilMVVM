using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyectoprogreso2.Models
{
    public class FacturaDescripcion
    {

        public int IdDescripcion { get; set; }
        public int Cantidad { get; set; }
        public double PrecioTotal { get; set; }
        public bool Status { get; set; }

        public ProductoColorTalla ProductoColorTalla { get; set; }

        public Factura Factura { get; set; }
    }
}
