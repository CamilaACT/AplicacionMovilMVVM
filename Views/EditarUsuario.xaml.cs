using Proyectoprogreso2.Models;
using Proyectoprogreso2.Service;
using Proyectoprogreso2.ViewModels;
using System.Collections.ObjectModel;

namespace Proyectoprogreso2;

public partial class EditarUsuario : ContentPage
{
    private readonly APIService _ApiService;
    private Cliente _cliente;
    private readonly EditarUsuarioViewModel _viewModel;
    public EditarUsuario(APIService apiservice)
    {
        InitializeComponent();
        _ApiService = apiservice;
        _viewModel = new EditarUsuarioViewModel();
        _viewModel.SetAPIService(apiservice);
        BindingContext = _viewModel;

    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        int respuesta = await _viewModel.LoadDatos();
        
       

    }
    private async void OnClickLogin(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void OnClickRegistrarse(object sender, EventArgs e)
    {
        int respuesta = await _viewModel.Editar();
        if(respuesta == 1)
        {
            await DisplayAlert("Yeiiiii", "Datos modificados correctamente", "OK");
            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Heeeeeeeeeeey", "Llene los campos correctamente, ashhhhh", "OK");
        }
        
    }
}