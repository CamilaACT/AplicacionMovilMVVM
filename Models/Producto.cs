﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyectoprogreso2.Models
{
    public class Producto
    {
        public int idProduto { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public TipoProducto tipoProducto { get; set; }
    }
}
