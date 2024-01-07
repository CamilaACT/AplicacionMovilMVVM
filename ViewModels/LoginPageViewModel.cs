using CommunityToolkit.Mvvm.ComponentModel;
using Proyectoprogreso2.Models;
using Proyectoprogreso2.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyectoprogreso2.ViewModels
{
    
    public partial class LoginPageViewModel : ObservableObject
    {
        private APIService _apiService;
        [ObservableProperty]
        public string nombreU;
        [ObservableProperty]
        public string contrasenia;
        public LoginPageViewModel()
        {

        }
        public void SetAPIService(APIService apiService)
        {
            _apiService = apiService;
        }
        public async Task<int> Login()
        {
            if (string.IsNullOrWhiteSpace(NombreU) || string.IsNullOrWhiteSpace(Contrasenia))
            {
                return -1;

            }
            else
            {
                Cliente clie = await _apiService.GetUsuario(NombreU, Contrasenia);
                if (clie != null)
                {
                    IntencionCompraDTO intencion = new IntencionCompraDTO
                    {
                        ClienteIdCliente=clie.IdCliente,
                        Fecha="13/12/2023"

                    };
                    IntencionCompra intencionrespuesta = await _apiService.PostIntencionCompra(intencion);

                    Preferences.Set("idusuario", clie.IdCliente);
                    Preferences.Set("CodigoIntencion", intencionrespuesta.IdIntencionCompra);
                    NombreU="";
                    Contrasenia="";
                    return 1;

                }
                else
                {
                    return 0;


                }
            }
        }
    }

}
