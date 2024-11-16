using System.Collections.ObjectModel;
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
        private readonly ReservasService serviceReservas;

        public ReservaViewModel(ReservasService service)
        {
            serviceReservas = service;
            Suministros = new ObservableCollection<Suministro>();

            SelectedSupplements = new ObservableCollection<Suministro>();
            CurrentUserId = Preferences.Get("IdUsuario", 0);

        }

        [ObservableProperty]
        private Cancha cancha;

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
        public ObservableCollection<Suministro> suministros;


        [ObservableProperty]
        public ObservableCollection<Suministro> selectedSupplements;

        [ObservableProperty]
        public DateTime dateTodayPlusThreeMonths = DateTime.Now.AddMonths(3);

        [ObservableProperty]
        public int iDUsuario;

        [ObservableProperty]
        public DateTime fechaInicio;

        [ObservableProperty]
        public TimeSpan horaInicio;

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
        public TimeSpan horaFinalizacion;

        [ObservableProperty]

        int currentUserId;



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
                Suministros = new ObservableCollection<Suministro>(suministrosList);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al obtener suministros: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", $"Error al obtener suministros: {ex.Message}", "OK");
            }
        }



        [RelayCommand]
        public async Task CreateReservaAsync()
        {
            var selectedIds = SelectedSupplements
                   .Select(s => s.IDSuministro)

                   .ToArray();
            try
            {
                string fechaReservaIso = FechaInicio.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");

                var horaFinal = HoraInicio.Add(TimeSpan.FromMinutes(Duracion)).ToString(@"hh\:mm\:ss");

                var reserva = new Reserva
                {
                    IDUsuario = CurrentUserId,
                    Duracion = Duracion,
                    Estado = "Confirmada",
                    EstadoPago = "Pagado",
                    FechaReserva = FechaInicio,
                    HoraInicio = HoraInicio.ToString(@"hh\:mm\:ss"),
                    HoraFinalizacion = horaFinal,
                    IDCancha = Cancha.IDCancha,
                    MetodoPago = MetodoPago,
                    MontoPagado = MontoPagado,
                    suministrosadicionales = selectedIds
                };

                var reservaJson = JsonSerializer.Serialize(new
                {
                    reserva.IDUsuario,
                    reserva.Duracion,
                    reserva.Estado,
                    reserva.EstadoPago,
                    FechaReserva = fechaReservaIso,
                    reserva.HoraInicio,
                    reserva.HoraFinalizacion,
                    reserva.IDCancha,
                    reserva.MetodoPago,
                    reserva.MontoPagado,
                    reserva.suministrosadicionales
                });

                Console.WriteLine("JSON enviado:");
                Console.WriteLine(reservaJson);

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
