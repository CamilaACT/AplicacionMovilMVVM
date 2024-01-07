//using AndroidX.Core.View.Accessibility;
using CommunityToolkit.Maui.Core;
using Proyectoprogreso2.Models;
using Proyectoprogreso2.Service;
using Proyectoprogreso2.ViewModels;

namespace Proyectoprogreso2;

public partial class LoginPage : ContentPage
{
    private readonly APIService _ApiService;
    private readonly LoginPageViewModel _viewModel;
    public LoginPage(APIService apiservice)
	{
		InitializeComponent();
        _viewModel = new LoginPageViewModel();
        _viewModel.SetAPIService(apiservice);
        BindingContext = _viewModel;
        _ApiService = apiservice;
    }

    private async void OnClickLogin(object sender, EventArgs e)
    {
        int respuesta = await _viewModel.Login();
        if (respuesta==-1)
        {
            await DisplayAlert("Campos vacíos", "Por favor, complete todos los campos.", "OK");
            
        }
        else
        {
            
            if (respuesta == 1)
            {
                await DisplayAlert("Bienvenido", "Inicio de sesión correcto", "OK");
                await Navigation.PushAsync(new ProductoPage(_ApiService));
            }
            else
            {
                await DisplayAlert("Lo sentimos", "Usuario o contraseña incorrectos", "OK");


            }
        }
    }

   

    private async void OnClickRegistrarse(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegistrarsePage(_ApiService));

    }
}