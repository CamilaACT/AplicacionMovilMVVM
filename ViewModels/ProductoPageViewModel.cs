using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Proyectoprogreso2.Models;
using Proyectoprogreso2.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyectoprogreso2.ViewModels
{
    public partial class ProductoPageViewModel : ObservableObject
    {

        private  APIService _apiService;
        [ObservableProperty]
        public string id;
        [ObservableProperty]
        public ObservableCollection<ProductoColorTalla> productos;

        public ProductoPageViewModel()
        {
            // Este constructor no toma argumentos y es necesario para XAML
        }


        public void SetAPIService(APIService apiService)
        {
            _apiService = apiService;
        }



        public async Task<ProductoColorTalla> Buscar()
        {
            if (string.IsNullOrWhiteSpace(Id) || !int.TryParse(Id, out int id))
            {

                return null;
            }

            ProductoColorTalla producto = await _apiService.GetProducto(id);

            if (producto != null)
            {

                return producto;
            }
            else
            {
                return null;

            }
        }

        public async void LoadProductosAsync()
        {
            Productos = new ObservableCollection<ProductoColorTalla>();
            var listaProducto = await _apiService.GetProductos();
            foreach (var product in listaProducto)
            {
                Productos.Add(product);
            }
        }

    }
}
