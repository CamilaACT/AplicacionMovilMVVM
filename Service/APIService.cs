using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Proyectoprogreso2.Models;

namespace Proyectoprogreso2.Service
{
    public class APIService
    {
        public static string _baseUrl;
        public HttpClient _httpClient;

        public APIService()
        {

            _baseUrl = "https://apiproyecto20231127081349.azurewebsites.net/";
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_baseUrl);
        }


        public async Task<bool> DeleteProducto(int IdProducto)
        {
            var response = await _httpClient.DeleteAsync($"/api/ProductoColorTalla/{IdProducto}");
            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return true;
            }
            return false;
        }

        public async Task<ProductoColorTalla> GetProducto(int IdProducto)
        {
            var response = await _httpClient.GetAsync($"/api/ProductoColorTalla/{IdProducto}");
            if (response.IsSuccessStatusCode)
            {
                var json_response = await response.Content.ReadAsStringAsync();
                ProductoColorTalla producto = JsonConvert.DeserializeObject<ProductoColorTalla>(json_response);
                return producto;
            }
            return null;
        }

        public async Task<List<ProductoColorTalla>> GetProductos()
        {
            var response = await _httpClient.GetAsync("/api/ProductoColorTalla");
            if (response.IsSuccessStatusCode)
            {
                var json_response = await response.Content.ReadAsStringAsync();
                List<ProductoColorTalla> productos = JsonConvert.DeserializeObject<List<ProductoColorTalla>>(json_response);
                return productos;
            }
            return new List<ProductoColorTalla>();

        }

        public async Task<Producto> PostProducto(Producto producto)
        {
            var content = new StringContent(JsonConvert.SerializeObject(producto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/Producto/", content);
            if (response.IsSuccessStatusCode)
            {
                var json_response = await response.Content.ReadAsStringAsync();
                Producto producto2 = JsonConvert.DeserializeObject<Producto>(json_response);
                return producto2;
            }
            return new Producto();
        }

        public async Task<Producto> PutProducto(int IdProducto, Producto producto)
        {
            var content = new StringContent(JsonConvert.SerializeObject(producto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"/api/Producto/{IdProducto}", content);
            if (response.IsSuccessStatusCode)
            {
                var json_response = await response.Content.ReadAsStringAsync();
                Producto producto2 = JsonConvert.DeserializeObject<Producto>(json_response);
                return producto2;
            }
            return new Producto();
        }
        public async Task<Cliente> GetUsuario(string Usu, string Contra)
        {
            var response = await _httpClient.GetAsync($"/api/Cliente/{Usu}/{Contra}");
            if (response.IsSuccessStatusCode)
            {
                var json_response = await response.Content.ReadAsStringAsync();
                Cliente cliente = JsonConvert.DeserializeObject<Cliente>(json_response);
                return cliente;
            }
            return null;
        }

        public async Task<Cliente> PostCliente(Cliente cliente)
        {
            var content = new StringContent(JsonConvert.SerializeObject(cliente), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/Cliente/", content);
            if (response.IsSuccessStatusCode)
            {
                var json_response = await response.Content.ReadAsStringAsync();
                Cliente cliente1 = JsonConvert.DeserializeObject<Cliente>(json_response);
                return cliente1;
            }
            return new Cliente();
        }

        // Método para crear la intención de compra o carrito
        public async Task<IntencionCompra> PostIntencionCompra(IntencionCompraDTO intencionCompraDTO)
        {
            var content = new StringContent(JsonConvert.SerializeObject(intencionCompraDTO), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/IntencionCompra/", content);
            if (response.IsSuccessStatusCode)
            {
                var json_response = await response.Content.ReadAsStringAsync();
                IntencionCompra intencionCompra = JsonConvert.DeserializeObject<IntencionCompra>(json_response);
                return intencionCompra;
            }
            return new IntencionCompra();
        }

        //Método para crear una descripcion de la intencion de compra o carrito
        public async Task<IntencionDescripcion> PostIntencionDescripcion(IntencionDescripcionDTO intencionDescripcionDTO)
        {
            var content = new StringContent(JsonConvert.SerializeObject(intencionDescripcionDTO), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/IntencionDescripcion/", content);
            if (response.IsSuccessStatusCode)
            {
                var json_response = await response.Content.ReadAsStringAsync();
                IntencionDescripcion intencionDescripcion = JsonConvert.DeserializeObject<IntencionDescripcion>(json_response);
                return intencionDescripcion;
            }
            return new IntencionDescripcion();

        }

        public async Task<List<IntencionDescripcion>> GetListaDescripcionIntencion(int IntencionCompraIdIntencionCompra)
        {
            var response = await _httpClient.GetAsync($"/api/IntencionDescripcion/PorCarrito/{IntencionCompraIdIntencionCompra}");

            if (response.IsSuccessStatusCode)
            {
                var json_response = await response.Content.ReadAsStringAsync();
                List<IntencionDescripcion> descripciones = JsonConvert.DeserializeObject<List<IntencionDescripcion>>(json_response);
                return descripciones;
            }
            return new List<IntencionDescripcion>();

        }

        public async Task<bool> DeleteIntencionDescripcion(int IdIntencionCompra)
        {
            var response = await _httpClient.DeleteAsync($"api/IntencionDescripcion/{IdIntencionCompra}");

            if (response.IsSuccessStatusCode)
            {
                var json_response = await response.Content.ReadAsStringAsync();
                List<IntencionDescripcion> descripciones = JsonConvert.DeserializeObject<List<IntencionDescripcion>>(json_response);
                return true;
            }
            return false;

        }

        //GENERAR LA FACTURA
        public async Task<Factura> PostGenerarFactura(int IntencionCompraIdIntencionCompra)
        {
            var content = new StringContent(JsonConvert.SerializeObject(IntencionCompraIdIntencionCompra), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/IntencionCompra/GenerarCompraFactura", content);
            if (response.IsSuccessStatusCode)
            {
                var json_response = await response.Content.ReadAsStringAsync();
                Factura factura = JsonConvert.DeserializeObject<Factura>(json_response);
                return factura;
            }
            return new Factura();
        }
        public async Task<bool> PostGenerarDetalleFactura(SolicitudDescripcion solicitudDescr)
        {
            var content = new StringContent(JsonConvert.SerializeObject(solicitudDescr), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/IntencionDescripcion/GenerarDescripcionFactura", content);
            if (response.IsSuccessStatusCode)
            {
                var json_response = await response.Content.ReadAsStringAsync();
  
                return true;
            }
            return false;
        }

        public async Task<double> GetPrecioTotal(int IntencionCompraIdIntencionCompra)
        {
            var response = await _httpClient.GetAsync($"/api/IntencionCompra/TotalIntencionCompra/{IntencionCompraIdIntencionCompra}");

            if (response.IsSuccessStatusCode)
            {
                var json_response = await response.Content.ReadAsStringAsync();
                double total = JsonConvert.DeserializeObject<double>(json_response);
                return total;
            }
            return 0.0;

        }

        public async Task<Cliente> GetCliente(int IdCliente)
        {
            var response = await _httpClient.GetAsync($"/api/Cliente/{IdCliente}");
            if (response.IsSuccessStatusCode)
            {
                var json_response = await response.Content.ReadAsStringAsync();
                Cliente cliente = JsonConvert.DeserializeObject<Cliente>(json_response);
                return cliente;
            }
            return null;
        }

        //Lista de Facturas
        public async Task<List<Factura>> GetListaFacturasCliente(int ClienteIdCliente)
        {
            var response = await _httpClient.GetAsync($"/api/Factura/PorCliente/{ClienteIdCliente}");

            if (response.IsSuccessStatusCode)
            {
                var json_response = await response.Content.ReadAsStringAsync();
                List<Factura> descripciones = JsonConvert.DeserializeObject<List<Factura>>(json_response);
                return descripciones;
            }
            return new List<Factura>();

        }
        public async Task<List<FacturaDescripcion>> GetDescripcionFactura(int FacturaIdFactura)
        {
            var response = await _httpClient.GetAsync($"/api/Descripcion/PorCarrito/{FacturaIdFactura}");

            if (response.IsSuccessStatusCode)
            {
                var json_response = await response.Content.ReadAsStringAsync();
                List<FacturaDescripcion> descripciones = JsonConvert.DeserializeObject<List<FacturaDescripcion>>(json_response);
                return descripciones;
            }
            return new List<FacturaDescripcion>();

        }
        public async Task<double> GetPrecioTotalFactura(int FacturaIdFactura)
        {
            var response = await _httpClient.GetAsync($"/api/Factura/TotalFactura/{FacturaIdFactura}");

            if (response.IsSuccessStatusCode)
            {
                var json_response = await response.Content.ReadAsStringAsync();
                double total = JsonConvert.DeserializeObject<double>(json_response);
                return total;
            }
            return 0.0;

        }
        public async Task<Cliente> PutCliente(Cliente cliente)
        {

            var IdCliente = Preferences.Get("idusuario", 0); ;
            var content = new StringContent(JsonConvert.SerializeObject(cliente), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"/api/Cliente/{IdCliente}", content);
            if (response.IsSuccessStatusCode)
            {
                var json_response = await response.Content.ReadAsStringAsync();
                Cliente cliente1 = JsonConvert.DeserializeObject<Cliente>(json_response);
                return cliente1;
            }
            return new Cliente();
        }
    }
}
