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
    public partial class RegistrarsePageViewModel : ObservableObject
    {
        private APIService _apiService;
        [ObservableProperty]
        public string nombre;
        [ObservableProperty]
        public string id;
        [ObservableProperty]
        public string apellido;
        [ObservableProperty]
        public string direccion;
        [ObservableProperty]
        public int tarjeta;
        [ObservableProperty]
        public string nombreU;
        [ObservableProperty]
        public string contrasenia;

        public RegistrarsePageViewModel()
        {

        }
        public void SetAPIService(APIService apiService)
        {
            _apiService = apiService;
        }
        public async Task<int> Registrarse()
        {
            if (string.IsNullOrWhiteSpace(Nombre) ||
                string.IsNullOrWhiteSpace(Id) ||
           string.IsNullOrWhiteSpace(Apellido) ||
           string.IsNullOrWhiteSpace(Direccion) ||
           string.IsNullOrWhiteSpace(NombreU) ||
           string.IsNullOrWhiteSpace(Contrasenia))
            {
                return -1;

            }
            else
            {
                Cliente Cli = new Cliente
                {
                    IdCliente = 0,
                    Nombre = Nombre,
                    Cedula = Id,
                    Apellido = Apellido,
                    Direccion=Direccion,
                    NumeroTarjeta= Tarjeta,
                    Login=NombreU,
                    Contrasenia=Contrasenia
                };
                await _apiService.PostCliente(Cli);
               
                return 1;
            }


        }
    }
}
