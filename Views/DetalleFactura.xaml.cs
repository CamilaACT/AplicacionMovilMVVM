using Proyectoprogreso2.Models;
using Proyectoprogreso2.Service;
using System.Collections.ObjectModel;

namespace Proyectoprogreso2;

public partial class DetalleFactura : ContentPage
{
    private Factura _factura;
    private readonly APIService _ApiService;
    public DetalleFactura(APIService apiservice)
	{
		InitializeComponent();
        _ApiService = apiservice;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        base.OnAppearing();
        _factura = BindingContext as Factura;
        int idfactura = _factura.IdFactura;
        List<FacturaDescripcion> listaDescripciones = await _ApiService.GetDescripcionFactura(idfactura);
        var products = new ObservableCollection<FacturaDescripcion>(listaDescripciones);
        ListaViewCarrito.ItemsSource = products;
        var totalprecio = await _ApiService.GetPrecioTotalFactura(idfactura);
        PrecioTotalCompra.Text= totalprecio.ToString();
    }
}