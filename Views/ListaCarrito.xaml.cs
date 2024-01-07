using Proyectoprogreso2.Models;
using Proyectoprogreso2.Service;
using Proyectoprogreso2.ViewModels;
using System.Collections.ObjectModel;

namespace Proyectoprogreso2;

public partial class ListaCarrito : ContentPage
{
    private readonly APIService _ApiService;
    public double totalpreciof;
    private readonly ListaCarritoViewModel _viewModel;
    public ListaCarrito(APIService apiservice)
	{
		InitializeComponent();
        _viewModel = new ListaCarritoViewModel();
        _viewModel.SetAPIService(apiservice);
        BindingContext = _viewModel;
        _ApiService = apiservice;

    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        LoadProductos();

        //int idintencioncompra = Preferences.Get("CodigoIntencion", 0);
        //List<IntencionDescripcion> listaProducto = await _ApiService.GetListaDescripcionIntencion(idintencioncompra);
        //var products = new ObservableCollection<IntencionDescripcion>(listaProducto);
        //ListaViewCarrito.ItemsSource = products;
        //var totalprecio = await _ApiService.GetPrecioTotal(idintencioncompra);
        //PrecioTotalCompra.Text= totalprecio.ToString();
        //totalpreciof=totalprecio;
    }

    private void LoadProductos()
    {
        _viewModel.LoadProductosCarritoAsync();
    }


    private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem == null)
            return;

        IntencionDescripcion selectedIntencion = ListaViewCarrito.SelectedItem as IntencionDescripcion;

        if (selectedIntencion != null)
        {
            await Navigation.PushAsync(new DetallesDescripcionIntencion(_ApiService)
            {
                BindingContext = selectedIntencion,
            });
        }

        // Desactiva la selección del elemento
        ListaViewCarrito.SelectedItem = null;
    }
    private async void ComprarClick(object sender, EventArgs e)
    {
        if (totalpreciof==0)
        {
            await DisplayAlert("Ashhhhh", "Primero agrega cosas a tu carrito :/", "OK");
        }
        else
        {
            await Navigation.PushAsync(new ConfirmacionCompra(_ApiService));
        }

        

    }


}