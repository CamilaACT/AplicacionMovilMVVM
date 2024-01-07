using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Proyectoprogreso2.Models;
using Proyectoprogreso2.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Proyectoprogreso2.ViewModels
{
    public partial class DetalleProductoPageViewModel : ObservableObject
    {
        private APIService _apiService;
        [ObservableProperty]
        public ProductoColorTalla producto;
        [ObservableProperty]
        public string nombre;
        [ObservableProperty]
        public string cantidad;
        [ObservableProperty]
        public int cantidadpicker;
        [ObservableProperty]
        public string color;
        [ObservableProperty]
        public int id;
        [ObservableProperty]
        public string descripcion;
        [ObservableProperty]
        public List<string> numeros;
        //private List<string> numeros;
        //public List<string> Numeros
        //{
        //    get => numeros;
        //    set => SetProperty(ref numeros, value);
        //}
        [ObservableProperty]
        public int cantidadMaximaStock;
        public DetalleProductoPageViewModel()
        {
           
        }
        public void SetAPIService(APIService apiService)
        {
            _apiService = apiService;
        }

        [RelayCommand]
        public async Task<int> OnClickAgregarAlCarrito()
        {
            int usuarioid = Preferences.Get("idusuario", 0);
            if (usuarioid == 0)
            {
                return -1;
            }
            else
            {
                var cantidadSeleccionada = Cantidadpicker;

                if (cantidadSeleccionada == 0)
                {
                    return 0;
                }
                else
                {
                    int idIntencionCompra = Preferences.Get("CodigoIntencion", 0);
                    cantidadSeleccionada = Cantidadpicker + 1;
                    IntencionDescripcionDTO intencionCompra = new IntencionDescripcionDTO
                    {
                        Cantidad = cantidadSeleccionada,
                        ProductoColorTallaIdProductoColorTalla = Producto.idProductoColorTalla,
                        IntencionCompraIdIntencionCompra = idIntencionCompra,
                    };

                    IntencionDescripcion intencionDescripcion = await _apiService.PostIntencionDescripcion(intencionCompra);
                    return 1;
                }
            }
        }
        public async Task LoadProductoAsync(int idProducto)
        {
            // Cargar el producto desde el servicio
            Producto = await _apiService.GetProducto(idProducto);

            // Actualizar las propiedades relacionadas con la vista
            Id = idProducto;
            Nombre = Producto.Producto.nombre;
            Cantidad = Producto.stock.ToString();
            CantidadMaximaStock = Producto.stock;
            Descripcion = Producto.Producto.descripcion;
            Color = Producto.ColorProducto.nombre;


        }

        public async Task<List<string>> CargarNumerosEnPicker(int idProducto)
        {

            Producto = await _apiService.GetProducto(idProducto);
            CantidadMaximaStock = Producto.stock;

            List<string> numeros = new List<string>();
            for (int i = 1; i <= CantidadMaximaStock; i++)
            {
                numeros.Add(i.ToString());
            }

            return numeros;
        }

    }
}
