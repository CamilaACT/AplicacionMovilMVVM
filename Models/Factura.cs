using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyectoprogreso2.Models
{
    public class Factura
    {
        public int IdFactura { get; set; }
        public string Fecha { get; set; }
        // Clave foránea para la relación uno a muchos con Cliente
        public int ClienteIdCliente { get; set; }
        public Cliente Cliente { get; set; }
    }
}
