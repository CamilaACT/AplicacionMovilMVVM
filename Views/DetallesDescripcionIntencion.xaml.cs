using Proyectoprogreso2.Models;
using Proyectoprogreso2.Service;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Proyectoprogreso2;

public partial class DetallesDescripcionIntencion : ContentPage
{

    private IntencionDescripcion _descripcion;
    private readonly APIService _ApiService;
    public DetallesDescripcionIntencion(APIService apiservice)
	{
        InitializeComponent();
        _ApiService = apiservice;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _descripcion = BindingContext as IntencionDescripcion;
        Cantidad.Text = _descripcion.Cantidad.ToString();
        PrecioTotal.Text = _descripcion.PrecioTotal.ToString();
        //ProductoColorTalla.Text=_descripcion.ProductoColorTalla.Producto.nombre.ToString();
    }
    private async void OnClickSalir(object sender, EventArgs e)
    {

        await Navigation.PopAsync();

    }
    private async void OnClickQuitarDelCarrito(object sender, EventArgs e)
    {
        var codigo=_descripcion.IdIntencionDescripcion;

        var desc= await _ApiService.DeleteIntencionDescripcion(codigo);
        await DisplayAlert("Yeiii", "Eliminaste el producto a tu carrito con éxito", "OK");
        await Navigation.PopAsync();

        //var toast = CommunityToolkit.Maui.Alerts.Toast.Make("Usuario o contraseña incorrecta", ToastDuration.Short, 14);

        //await toast.Show();

    }
}