using Proyectoprogreso2.Models;
using Proyectoprogreso2.Service;
using Proyectoprogreso2.ViewModels;

namespace Proyectoprogreso2;

public partial class RegistrarsePage : ContentPage
{
    private readonly APIService _ApiService;
    private readonly RegistrarsePageViewModel _viewModel;

    public RegistrarsePage(APIService apiservice)
    {
		InitializeComponent();
        _ApiService = apiservice;
        _viewModel = new RegistrarsePageViewModel();
        _viewModel.SetAPIService(apiservice);
        BindingContext = _viewModel;
    }

    private async void OnClickLogin(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void OnClickRegistrarse(object sender, EventArgs e)
    {
        int respuesta = await _viewModel.Registrarse();
        if (respuesta == -1) 
        {
            await DisplayAlert("Campos vacíos", "Por favor, complete todos los campos.", "OK");
        }
        else
        {
            await DisplayAlert("Exito", "Cliente registrado correctamente", "OK");
            await Navigation.PopAsync();
        }


    }
}