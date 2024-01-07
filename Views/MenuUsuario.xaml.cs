using Microsoft.Maui.Controls;
using Proyectoprogreso2.Service;

namespace Proyectoprogreso2
{
    public partial class MenuUsuario : ContentPage
    {
        private readonly APIService _ApiService;
        public  MenuUsuario(APIService apiservice)
        {
            _ApiService = apiservice;

            InitializeComponent();
            
        }

        private async void OnAdministrarPerfilClicked(object sender, EventArgs e)
        {
            // Acciones cuando se hace clic en "Administrar Perfil"
            await Navigation.PushAsync(new EditarUsuario(_ApiService));
        }

        private async void OnVisualizarFacturaClicked(object sender, EventArgs e)
        {
            // Acciones cuando se hace clic en "Visualizar Factura"

            DisplayAlert("Acción", "Visualizar Factura", "OK");
            await Navigation.PushAsync(new ListaFactura(_ApiService));
        }

        private async void OnCerrarSesionClicked(object sender, EventArgs e)
        {
            // Acciones cuando se hace clic en "Cerrar Sesión"
            DisplayAlert("Acción", "Cerrar Sesión", "OK");
            Preferences.Set("idusuario", 0);
            Preferences.Set("CodigoIntencion", 0);
            await Navigation.PopToRootAsync();
        }
    }
}
