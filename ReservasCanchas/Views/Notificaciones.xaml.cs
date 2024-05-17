using System.Collections.ObjectModel;
using System.Text.Json;
using ReservasCanchas.Models;

namespace ReservasCanchas.Views
{
    public partial class Notificaciones : ContentPage
    {
        public ObservableCollection<Notificacion> NotificacionesList { get; set; }
        HttpClient httpClient { get; set; }

        string URL = "https://reserva-canchas.vercel.app";

        public Notificaciones()
        {
            InitializeComponent();
            NotificacionesList = new ObservableCollection<Notificacion>();
            httpClient = new HttpClient();
            BindingContext = this;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await getNotificaciones();
        }

        async Task getNotificaciones()
        {
            try
            {
                var response = await httpClient.GetAsync($"{URL}/notificacion/1");
                if (response.IsSuccessStatusCode)
                {
                    var productData = await response.Content.ReadAsStringAsync();
                    var notificaciones = JsonSerializer.Deserialize<ObservableCollection<Notificacion>>(productData);
                    if (notificaciones != null)
                    {
                        NotificacionesList.Clear();
                        foreach (var notificacion in notificaciones)
                        {
                            NotificacionesList.Add(notificacion);
                        }
                    }
                }
                else
                {
                    NotificacionesList.Clear();
                    throw new HttpRequestException($"Error al obtener notificaciones: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                NotificacionesList.Clear();
            }
        }
    }
}
