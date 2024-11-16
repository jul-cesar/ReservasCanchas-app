using System.Diagnostics;
using System.Globalization;
using System.Text.Json;
using Plugin.Maui.Calendar.Models;
using ReservasCanchas.Models;
using UraniumUI.Dialogs;

namespace ReservasCanchas.Views;

public partial class ReservasCancha : ContentPage, IQueryAttributable
{
    public EventCollection Events { get; set; }
    public CultureInfo Culture { get; set; }
    public Cancha currentCancha { get; set; }
    public IDialogService DialogService { get; }
    private static readonly HttpClient client = new HttpClient();

    public ReservasCancha()
    {
        InitializeComponent();
        Culture = new CultureInfo("es-CO");
        Events = new EventCollection();
        BindingContext = this;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadReservas();
    }

    private async Task LoadReservas()
    {
        try
        {
            var reservas = await GetReservasCanchasAsync();
            AddReservasToCalendar(reservas);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudieron cargar las reservas: {ex.Message}", "OK");
        }
    }

    public async Task<List<ReservasResponse>> GetReservasCanchasAsync()
    {
        try
        {
            var reservasList = await GetReservasCancha(currentCancha.IDCancha);

            if (reservasList == null)
            {
                Debug.WriteLine("La lista de reservas es nula");
                return new List<ReservasResponse>();
            }
            return reservasList;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error al obtener reservas: {ex.Message}");
            await Shell.Current.DisplayAlert("Error", $"Error al obtener reservas: {ex.Message}", "OK");
            return new List<ReservasResponse>();
        }
    }

    public async Task<List<ReservasResponse>> GetReservasCancha(int id)
    {
        string url = $"https://67e7-2800-e2-407f-fd96-4daa-3067-13f5-605c.ngrok-free.app/reserva/{id}";

        try
        {
            var response = await client.GetStringAsync(url);
            var reservas = JsonSerializer.Deserialize<List<ReservasResponse>>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return reservas;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error al deserializar las reservas: {ex.Message}");
            return new List<ReservasResponse>();
        }
    }

    private void AddReservasToCalendar(List<ReservasResponse> reservas)
    {
        foreach (var reserva in reservas)
        {
            if (reserva.canchas == null || reserva.usuarios == null)
            {
                Debug.WriteLine("Reserva canchas o usuarios es nulo");
                continue;
            }

            DateTime fechaReserva = reserva.FechaReserva.Date;

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

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("Cancha"))
        {
            currentCancha = query["Cancha"] as Cancha;
        }
    }

    public class EventModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}
