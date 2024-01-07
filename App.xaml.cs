using Proyectoprogreso2.Service;

namespace Proyectoprogreso2
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            APIService ApiService = new APIService();
            MainPage = new NavigationPage(new ProductoPage(ApiService));
        }
    }
}