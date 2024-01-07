using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyectoprogreso2.Models
{
    public class IntencionCompra
    {
        public int IdIntencionCompra { get; set; }
        public string Fecha { get; set; }
        public Cliente Cliente { get; set; }
    }
}
