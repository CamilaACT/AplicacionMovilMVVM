using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyectoprogreso2.Models
{
    public class FacturaDTO
    {
        public int IdFactura { get; set; }
        public string Fecha { get; set; }

        // Clave foránea para la relación uno a muchos con Cliente
        public int ClienteIdCliente { get; set; }
    }
}
