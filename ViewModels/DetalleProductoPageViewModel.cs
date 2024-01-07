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
        public async Task OnClickAgregarAlCarrito()
        {
            int usuarioid = Preferences.Get("idusuario", 0);
            if (usuarioid == 0)
            {
                // Mostrar página de inicio de sesión
                // Puedes implementar esta lógica según tu necesidad
                // NavigationService.NavigateToLoginPage();
            }
            else
            {
                var cantidadSeleccionada = cantidadpicker + 1;

                if (cantidadSeleccionada == 0)
                {
                    // Mostrar mensaje de advertencia
                    // Puedes implementar esta lógica según tu necesidad
                    // DisplayAlert("Espera", "Agrega algo al carrito primero :)", "OK");
                }
                else
                {
                    int idIntencionCompra = Preferences.Get("CodigoIntencion", 0);

                    IntencionDescripcionDTO intencionCompra = new IntencionDescripcionDTO
                    {
                        Cantidad = cantidadSeleccionada,
                        ProductoColorTallaIdProductoColorTalla = Producto.idProductoColorTalla,
                        IntencionCompraIdIntencionCompra = idIntencionCompra,
                    };

                    IntencionDescripcion intencionDescripcion = await _apiService.PostIntencionDescripcion(intencionCompra);
                    // Mostrar mensaje de éxito
                    // Puedes implementar esta lógica según tu necesidad
                    // DisplayAlert("Éxito", "Agregaste el producto a tu carrito con éxito", "OK");

                    // Cerrar la página de detalles
                    // Puedes implementar esta lógica según tu necesidad
                    // NavigationService.PopAsync();
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

            // Cargar los números en el Picker
            //CargarNumerosEnPicker();
        }
        //[RelayCommand]
        //public void CargarNumerosEnPicker()
        //{
        //    Numeros = new List<string>();
        //    for (int i = 1; i <= CantidadMaximaStock; i++)
        //    {
        //        Numeros.Add(i.ToString());
        //    }
        //}
        public async Task<List<string>> CargarNumerosEnPicker(int idProducto)
        {
            // Cargar el producto desde el servicio
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
