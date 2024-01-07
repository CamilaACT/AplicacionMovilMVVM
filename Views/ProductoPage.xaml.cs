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
        _ApiService = apiservice;
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



    //private async void Btn_Buscar(object sender, EventArgs e)
    //{
    //    // Verificar si el Entry "Name" est� vac�o
    //    if (string.IsNullOrWhiteSpace(ID.Text) || !int.TryParse(ID.Text, out int id))
    //    {
    //        // Entry est� vac�o, puedes mostrar un mensaje o simplemente salir del m�todo
    //        DisplayAlert("Advertencia", "Por favor, ingrese un ID v�lido.", "OK");
    //        return;
    //    }

    //    // Continuar con la b�squeda ya que el Entry "Name" contiene alg�n valor
    //    ProductoColorTalla p = await _ApiService.GetProducto(Int32.Parse(ID.Text));

    //    // Verificar si se encontr� un producto antes de navegar a la p�gina de detalles
    //    if (p != null )
    //    {
    //        await Navigation.PushAsync(new DetalleProductoPage(_ApiService)
    //        {
    //            BindingContext = p,
    //        });
    //    }
    //    else
    //    {
    //        // Mostrar un mensaje si no se encontr� ning�n producto con el ID proporcionado
    //        DisplayAlert("Advertencia", "No se encontr� ning�n producto con el ID proporcionado.", "OK");
    //    }
    //}

    private async void OnUsuarioClicked(object sender, SelectedItemChangedEventArgs e)
    {

        ProductoColorTalla resultado = await _viewModel.Buscar();
    

        if (resultado !=null)
        {
            // B�squeda exitosa, navegar a la p�gina de detalles
            await Navigation.PushAsync(new DetalleProductoPage(_ApiService,resultado.Producto.idProduto));
            
        }
        else
        {
            // Mostrar mensaje de advertencia si la b�squeda no tuvo �xito
            DisplayAlert("Advertencia", "No se encontr� ning�n producto con el ID proporcionado.", "OK");
        }
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        LoadProductos();
        //List<ProductoColorTalla> listaProducto = await _ApiService.GetProductos();
        //var products = new ObservableCollection<ProductoColorTalla>(listaProducto);
        //ListaProducto.ItemsSource = products;
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
        else {

            await Navigation.PushAsync(new MenuUsuario(_ApiService));
        }
        
    }


}