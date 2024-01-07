using Proyectoprogreso2.Models;
using Proyectoprogreso2.Service;
using System.Collections.ObjectModel;

namespace Proyectoprogreso2;

public partial class ConfirmacionCompra : ContentPage
{
    private readonly APIService _ApiService;
    public ConfirmacionCompra(APIService apiservice)
	{
        
		InitializeComponent();
        _ApiService = apiservice;
    }
    protected async override void OnAppearing()
    {
        base.OnAppearing();

        int IdUsuario = Preferences.Get("idusuario", 0);
        Cliente usuario = await _ApiService.GetCliente(IdUsuario);
        Nombre.Text= usuario.Nombre;
        int idintencioncompra = Preferences.Get("CodigoIntencion", 0);
        var totalprecio = await _ApiService.GetPrecioTotal(idintencioncompra);
        TotalCompra.Text= totalprecio.ToString();
        TarjetaRegistrada.Text=OcultarDigitosTarjeta(usuario.NumeroTarjeta);
        List<IntencionDescripcion> listaProducto = await _ApiService.GetListaDescripcionIntencion(idintencioncompra);
        var products = new ObservableCollection<IntencionDescripcion>(listaProducto);
        ListaViewCarrito.ItemsSource = products;
    }

    private string OcultarDigitosTarjeta(int numeroTarjeta)
    {

        string numeroTarjetaStr = numeroTarjeta.ToString();

        if (string.IsNullOrEmpty(numeroTarjetaStr) || numeroTarjetaStr.Length < 4)
        {
            // Manejo de caso inválido o tarjeta demasiado corta
            return string.Empty;
        }

        string ultimosTresDigitos = numeroTarjetaStr.Substring(numeroTarjetaStr.Length - 3);


        string ocultarDigitos = new string('*', numeroTarjetaStr.Length - 3);

        string numeroTarjetaOculto = ocultarDigitos + ultimosTresDigitos;

        return numeroTarjetaOculto;
    }
    private async void FinalizarClick(object sender, EventArgs e)
    {
        int idintencioncompra = Preferences.Get("CodigoIntencion", 0);
        Factura factura = await _ApiService.PostGenerarFactura(idintencioncompra);
        int idFactura = factura.IdFactura;
        SolicitudDescripcion soli = new SolicitudDescripcion
        {
            IntencionCompraIdIntencionCompra= idintencioncompra,
            FacturaIdFactura=idFactura,
        };
        bool detallegenerar = await _ApiService.PostGenerarDetalleFactura(soli);
        if(detallegenerar)
        {
            int idCliente = Preferences.Get("idusuario", 0);
            await DisplayAlert("Éxito", "Realizaste una compra con exito, vuelve pronto", "OK");
            IntencionCompraDTO intencion = new IntencionCompraDTO
            {
                ClienteIdCliente=idCliente,
                Fecha="13/12/2023"

            };
            IntencionCompra intencionrespuesta = await _ApiService.PostIntencionCompra(intencion);
            Preferences.Set("CodigoIntencion", intencionrespuesta.IdIntencionCompra);
            await Navigation.PopToRootAsync();
        }
        else
        {
            await DisplayAlert("Lo sentimos", "Algo fue mal con tu compra", "OK");
        }



    }

}