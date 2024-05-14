using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReservasCanchas.Models;
using ReservasCanchas.Services;

namespace ReservasCanchas.ViewModels
{
    public partial class ReservaViewModel : ObservableObject
    {
        ReservasService serviceReservas;

        public ReservaViewModel(ReservasService service, Cancha cancha)
        {
            this.serviceReservas = service;
            this.CanchaSeleccionada = cancha;

        }
        [ObservableProperty]
        Cancha canchaSeleccionada;

        [ObservableProperty]
        private string[] items = new string[]
   {
        "Tarjeta de Crédito",
        "Tarjeta de Débito",
        "Transferencia Bancaria",
        "Efectivo"
   };

        [ObservableProperty]
        public int iDUsuario;
        [ObservableProperty]
        public int iDCancha;
        [ObservableProperty]
        public string fechaInicio;
        [ObservableProperty]
        public string horaInicio;
        [ObservableProperty]
        public int duracion;
        [ObservableProperty]
        public string estado;
        [ObservableProperty]
        public string metodoPago;
        [ObservableProperty]
        public string estadoPago;
        [ObservableProperty]
        public float montoPagado;
        [ObservableProperty]
        public int[] suministrosadicionales;

        [RelayCommand]
        async Task createReservaAsync()
        {
            try
            {
                await serviceReservas.crearReserva(new Models.Reserva
                {
                    Duracion = this.Duracion,
                    Estado = this.Estado,
                    EstadoPago = this.EstadoPago,
                    FechaInicio = this.FechaInicio,
                    HoraInicio = this.HoraInicio,
                    IDCancha = this.IDCancha,
                    IDUsuario = this.IDUsuario,
                    MetodoPago = this.MetodoPago,
                    MontoPagado = this.MontoPagado,
                    suministrosadicionales = this.Suministrosadicionales
                });

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error", "Error trayendo las canchas", "ok");
            }
        }
    }
}
