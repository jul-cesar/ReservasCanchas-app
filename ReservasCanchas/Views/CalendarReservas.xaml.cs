using System.Globalization;
using System.Text.Json;
using Plugin.Maui.Calendar.Models;
using ReservasCanchas.Models;


namespace ReservasCanchas.Views;

public partial class CalendarReservas : ContentPage
{
    public EventCollection Events { get; set; }
    public CultureInfo Culture { get; set; }

    public CalendarReservas()
    {
        InitializeComponent();
        Culture = new CultureInfo("es-CO");
        Events = new EventCollection();
        BindingContext = this;

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadReservas();
    }

    private async void LoadReservas()
    {
        try
        {
            var reservas = await GetReservasFromBackend();
            AddReservasToCalendar(reservas);
        }
        catch (Exception ex)
        {
            // Manejar la excepción (por ejemplo, mostrar un mensaje de error)
            await DisplayAlert("Error", $"No se pudieron cargar las reservas: {ex.Message}", "OK");
        }
    }

    private async Task<List<ReservasResponse>> GetReservasFromBackend()
    {
        // URL de tu endpoint
        string url = "https://reserva-canchas.vercel.app/reserva";

        using (HttpClient client = new HttpClient())
        {
            var response = await client.GetStringAsync(url);
            var reservas = JsonSerializer.Deserialize<List<ReservasResponse>>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return reservas;
        }
    }

    private void AddReservasToCalendar(List<ReservasResponse> reservas)
    {
        foreach (var reserva in reservas)
        {
            DateTime fechaReserva = reserva.FechaReserva.Date; // Use only the date part

            if (!Events.ContainsKey(fechaReserva))
            {
                Events[fechaReserva] = new List<EventModel>();
            }

            ((List<EventModel>)Events[fechaReserva]).Add(new EventModel
            {
                Name = $"Reserva: {reserva.canchas.Nombre}",
                Description = $"Reservado por: {reserva.usuarios.Nombre} {reserva.usuarios.Apellido}\n" +
                              $"Hora: {reserva.HoraInicio}\n" +
                              $"Hora finalizacion: {reserva.HoraFinalizacion}\n" +

                              $"Estado: {reserva.Estado}"
            });
        }

        OnPropertyChanged(nameof(Events));
    }
    public class EventModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
