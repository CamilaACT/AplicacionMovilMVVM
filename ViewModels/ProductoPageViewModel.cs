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

        public ProductoPageViewModel()
        {
            // Este constructor no toma argumentos y es necesario para XAML
        }

        // Este método puede ser utilizado para establecer el APIService después de la creación del ViewModel
        public void SetAPIService(APIService apiService)
        {
            _apiService = apiService;
        }


        [ObservableProperty]
        public string id;
        [ObservableProperty]
        public ObservableCollection<ProductoColorTalla> productos;

        //[RelayCommand]
        public async Task<ProductoColorTalla> Buscar()
        {
            if (string.IsNullOrWhiteSpace(Id) || !int.TryParse(Id, out int id))
            {
                // Handle invalid input
                return null;
            }

            ProductoColorTalla producto = await _apiService.GetProducto(id);

            if (producto != null)
            {
                // Handle successful product retrieval
                return producto;
            }
            else
            {
                return null;
                // Handle no product found
            }
        }
        //[RelayCommand]
        public async void LoadProductosAsync()
        {
            Productos = new ObservableCollection<ProductoColorTalla>();
            var listaProducto = await _apiService.GetProductos();
            //Productos.Clear(); // Clear existing items (optional)
            foreach (var product in listaProducto)
            {
                Productos.Add(product);
            }
        }

    }
}
