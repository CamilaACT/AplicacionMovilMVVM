using CommunityToolkit.Maui.Core;
using Proyectoprogreso2.Models;
using Proyectoprogreso2.Service;
using Proyectoprogreso2.ViewModels;
using System.Collections.ObjectModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Proyectoprogreso2;

public partial class DetalleProductoPage : ContentPage
{
    private readonly APIService _ApiService;
    private readonly int _idProd;
    private readonly DetalleProductoPageViewModel _viewModel;

    public DetalleProductoPage(APIService apiservice,int idproducto)
    {
        InitializeComponent();
        _ApiService = apiservice;
        _viewModel = new DetalleProductoPageViewModel();
        _viewModel.SetAPIService(apiservice);
        _idProd=idproducto;
        BindingContext = _viewModel;
    }


    protected async override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.LoadProductoAsync(_idProd);
        List<string> numeros = await _viewModel.CargarNumerosEnPicker(_idProd);
        CantidadPicker.ItemsSource = numeros;

    }

    private async void OnClickSalir(object sender, EventArgs e)
    {

        await Navigation.PopAsync();

    }

    private async void OnClickAgregarAlCarrito(object sender, EventArgs e)
    {
        int respuesta = await _viewModel.OnClickAgregarAlCarrito();
        if(respuesta == -1)
        {
            Navigation.PushAsync(new LoginPage(_ApiService));
        }
        else
        {
            if (respuesta==0)
            {
                await DisplayAlert("Esperaaa", "Agrega algo al carrito primero :)", "OK");
            }
            else
            {
                await DisplayAlert("Yeiii", "Agregaste el producto a tu carrito con éxito", "OK");
                await Navigation.PopAsync();
            }
        }

    }


}