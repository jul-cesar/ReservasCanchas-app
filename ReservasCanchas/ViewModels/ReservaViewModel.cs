using System.Diagnostics;
using System.Text;
using System.Text.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReservasCanchas.Models;
using ReservasCanchas.Services;

namespace ReservasCanchas.ViewModels
{
    [QueryProperty("Cancha", "Cancha")]

    public partial class ReservaViewModel : ObservableObject
    {
        ReservasService serviceReservas;

        public ReservaViewModel(ReservasService service)
        {
            this.serviceReservas = service;
            Suministros = new List<string>();

        }



        [ObservableProperty]
        Cancha cancha;

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
        public List<string> suministros;
        public List<Suministro> suministrosObjs;



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

        [ObservableProperty]
        public string[] sums;

        [ObservableProperty]
        public TimeSpan horaFinalizacion;


        public string FechaInicioString => FechaInicio.ToString("dd/MM/yyyy");

        public async Task GetSuministrosAsync()
        {
            try
            {
                var suministrosList = await serviceReservas.GetSuministros();

                if (suministrosList == null)
                {
                    Debug.WriteLine("La lista de suministros es nula");
                    return;
                }
                suministrosObjs = suministrosList;

                foreach (var sum in suministrosList)
                {
                    Suministros.Add(sum.TipoSuministro);
                }


            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al obtener suministros: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", $"Error al obtener suministros: {ex.Message}", "OK");
            }
        }

        [RelayCommand]
        async Task createReservaAsync()
        {

            try
            {

                string fechaReservaIso = FechaInicio.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");


                var horaFinal = HoraInicio.Add(TimeSpan.FromMinutes(Duracion)).ToString(@"hh\:mm\:ss");

                var reserva = new Reserva
                {
                    IDUsuario = 1,
                    Duracion = Duracion,
                    Estado = "Confirmada",
                    EstadoPago = "Pagado",
                    FechaReserva = FechaInicio,
                    HoraInicio = HoraInicio.ToString(@"hh\:mm\:ss"),
                    HoraFinalizacion = horaFinal,
                    IDCancha = Cancha.IDCancha,
                    MetodoPago = MetodoPago,
                    MontoPagado = MontoPagado,
                    suministrosadicionales = SuministrosAdicionales
                };

                var reservaJson = JsonSerializer.Serialize(new
                {
                    reserva.IDUsuario,
                    reserva.Duracion,
                    reserva.Estado,
                    reserva.EstadoPago,
                    FechaReserva = fechaReservaIso,
                    HoraInicio = reserva.HoraInicio,
                    HoraFinalizacion = reserva.HoraFinalizacion,
                    reserva.IDCancha,
                    reserva.MetodoPago,
                    reserva.MontoPagado,
                    reserva.suministrosadicionales
                });


                Console.WriteLine("JSON enviado:");
                Console.WriteLine(reservaJson);

                // Send the JSON to the service
                var content = new StringContent(reservaJson, Encoding.UTF8, "application/json");
                await serviceReservas.CrearReserva(content);



            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error", "Error creando la reserva", "OK");
            }
        }
    }
}
