using Proyectoprogreso2.Models;
using Proyectoprogreso2.Service;
using Proyectoprogreso2.ViewModels;
using System.Collections.ObjectModel;


namespace Proyectoprogreso2;

public partial class ProductoPage : ContentPage
{

    private readonly APIService _ApiService;
    private readonly ProductoPageViewModel _viewModel;
    public ProductoPage(APIService apiservice)
    {
        InitializeComponent();
        _viewModel = new ProductoPageViewModel();
        _viewModel.SetAPIService(apiservice);
        BindingContext = _viewModel;
        _ApiService=apiservice;
        int usuarioid = Preferences.Get("idusuario", 0);
        if (usuarioid==0)
        {
            Preferences.Set("idusuario", 0);
            Preferences.Set("CodigoIntencion", 0);
         
        }
     
          

    }
    private void LoadProductos()
    {
        _viewModel.LoadProductosAsync();
    }


    private async void OnCarritoClicked(object sender, EventArgs e)
    {
        int usuarioid = Preferences.Get("idusuario", 0);
        if (usuarioid==0)
        {
            Navigation.PushAsync(new LoginPage(_ApiService));
        }
        else
        {

            await Navigation.PushAsync(new ListaCarrito(_ApiService));
        }
       
    }


    private async void OnBuscaClicked(object sender, EventArgs e)
    {

        ProductoColorTalla resultado = await _viewModel.Buscar();
    

        if (resultado !=null)
        {
            // Búsqueda exitosa, navegar a la página de detalles
            await Navigation.PushAsync(new DetalleProductoPage(_ApiService,resultado.idProductoColorTalla));
            
        }
        else
        {
            // Mostrar mensaje de advertencia si la búsqueda no tuvo éxito
            DisplayAlert("Advertencia", "No se encontró ningún producto con el ID proporcionado.", "OK");
        }
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        LoadProductos();

    }
    private async void OnClickShowDetails(object sender, SelectedItemChangedEventArgs e)
    {
     
        ProductoColorTalla producto = e.SelectedItem as ProductoColorTalla;
        await Navigation.PushAsync(new DetalleProductoPage(_ApiService, producto.idProductoColorTalla));
    }

    private async void OnUsuarioClicked(object sender, EventArgs e)
    {
        int usuarioid = Preferences.Get("idusuario", 0);
        if (usuarioid==0)
        {
            Navigation.PushAsync(new LoginPage(_ApiService));
        }
        else
        {

            await Navigation.PushAsync(new MenuUsuario(_ApiService));
        }

    }


}