using Proyectoprogreso2.Models;
using Proyectoprogreso2.Service;
using System.Collections.ObjectModel;

namespace Proyectoprogreso2;

public partial class ListaFactura : ContentPage
{
    private readonly APIService _ApiService;
    public ListaFactura(APIService apiservice)
	{
		InitializeComponent();
        _ApiService = apiservice;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        int idusuario = Preferences.Get("idusuario", 0);
        List<Factura> listaFacturaas = await _ApiService.GetListaFacturasCliente(idusuario);
        var listadefacturas = new ObservableCollection<Factura>(listaFacturaas);
        ListaViewFactura.ItemsSource = listadefacturas;

    }

    private async void ListaViewFactura_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        Factura selectedIntencion = ListaViewFactura.SelectedItem as Factura;

        if (selectedIntencion != null)
        {
            await Navigation.PushAsync(new DetalleFactura(_ApiService)
            {
                BindingContext = selectedIntencion,
            });
        }
    }
}