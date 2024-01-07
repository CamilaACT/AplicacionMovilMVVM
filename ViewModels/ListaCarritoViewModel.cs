using CommunityToolkit.Mvvm.ComponentModel;
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
    public partial class ListaCarritoViewModel : ObservableObject
    {
        private APIService _apiService;
        [ObservableProperty]
        public ObservableCollection<IntencionDescripcion> listaProducto;
        [ObservableProperty]
        public double totalpreciof;
        [ObservableProperty]
        public string precioTotalCompra;
        public ListaCarritoViewModel()
        {

        }
        public void SetAPIService(APIService apiService)
        {
            _apiService = apiService;

        }
        public async void LoadProductosCarritoAsync()
        {
            int idintencioncompra = Preferences.Get("CodigoIntencion", 0);
            
            //ListaViewCarrito.ItemsSource = products;
            ListaProducto = new ObservableCollection<IntencionDescripcion>();
            var listaProducto = await _apiService.GetListaDescripcionIntencion(idintencioncompra);
            foreach (var product in listaProducto)
            {
                ListaProducto.Add(product);
            }

            var totalprecio = await _apiService.GetPrecioTotal(idintencioncompra);
            PrecioTotalCompra= totalprecio.ToString();
            totalpreciof=totalprecio;
        }
    }
}

