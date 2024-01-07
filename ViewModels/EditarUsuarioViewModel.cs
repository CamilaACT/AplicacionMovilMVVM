using CommunityToolkit.Mvvm.ComponentModel;
using Proyectoprogreso2.Models;
using Proyectoprogreso2.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Proyectoprogreso2.ViewModels
{
    public partial class EditarUsuarioViewModel : ObservableObject
    {
        private APIService _apiService;
        [ObservableProperty]
        public Cliente cliente;
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
        public EditarUsuarioViewModel()
        {

        }
        public void SetAPIService(APIService apiService)
        {
            _apiService = apiService;
        }
        public async Task<int> LoadDatos()
        {

            int idclient = Preferences.Get("idusuario", 0);
            Cliente= await _apiService.GetCliente(idclient);
            Id=Cliente.IdCliente.ToString();
            Apellido=Cliente.Apellido;
            Nombre=Cliente.Nombre;
            Direccion=Cliente.Direccion;
            Tarjeta=Cliente.NumeroTarjeta;
            NombreU=Cliente.Login;
            Contrasenia=Cliente.Contrasenia;

            return 1;
        }
        public async Task<int> Editar()
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
                await _apiService.PutCliente(Cli);

                return 1;
            }


        }


    }
}
