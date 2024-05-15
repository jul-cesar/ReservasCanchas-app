using System.Diagnostics;
using System.Text;
using System.Text.Json;
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
        "TarjetaDeCrédito",
        "TarjetaDeDebito",
        "TransferenciaBancaria",
        "Efectivo"
   };
        [ObservableProperty]
        public DateTime dateToday = DateTime.Today;

        [ObservableProperty]

        public DateTime dateTodayPlusThreeMonths = DateTime.Now.AddMonths(3);


        [ObservableProperty]
        public int iDUsuario;

        [ObservableProperty]
        public DateTime fechaInicio;
        [ObservableProperty]
        public TimeSpan horaInicio;
        [ObservableProperty]
        private TimeSpan horaInicioTimeSpan;
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
        public int[] suministrosAdicionales;

        public string FechaInicioString => FechaInicio.ToString("dd/MM/yyyy");


        [RelayCommand]
        async Task createReservaAsync()
        {


            try
            {
                var reserva = new Reserva
                {
                    IDUsuario = 1,
                    Duracion = 120,
                    Estado = "Confirmada",
                    EstadoPago = "Pagado",
                    FechaReserva = FechaInicio,
                    HoraInicio = HoraInicio.ToString(),
                    IDCancha = CanchaSeleccionada.IDCancha,
                    MetodoPago = MetodoPago,
                    MontoPagado = MontoPagado,
                    suministrosadicionales = SuministrosAdicionales ?? new int[] { }
                };

                var reservaJson = JsonSerializer.Serialize(new
                {
                    reserva.IDUsuario,
                    reserva.Duracion,
                    reserva.Estado,
                    reserva.EstadoPago,
                    FechaReserva = reserva.FechaReserva.ToString("o"), // ISO 8601 format
                    reserva.HoraInicio,
                    reserva.IDCancha,
                    reserva.MetodoPago,
                    reserva.MontoPagado,
                    reserva.suministrosadicionales
                });

                // Log the JSON being sent
                Console.WriteLine("JSON enviado:");
                Console.WriteLine(reservaJson);

                // Send the JSON to the service
                var content = new StringContent(reservaJson, Encoding.UTF8, "application/json");
                await serviceReservas.CrearReserva(content);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error", "Error trayendo las canchas", "OK");
            }

        }
    }
}
