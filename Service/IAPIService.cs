using Proyectoprogreso2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyectoprogreso2.Service
{
    internal interface IAPIService
    {
        public Task<List<Producto>> GetProductos();

        public Task<Producto> GetProducto(int id);
        public Task<Producto> PostProducto(Producto producto);
        public Task<Producto> PutProducto(int IdProducto, Producto producto);
        public Task<Boolean> DeleteProducto(int IdProducto);
    }
}
