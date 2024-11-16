using System.Collections.ObjectModel;
using System.Text.Json;
using ReservasCanchas.Models;

namespace ReservasCanchas.Views
{
    public partial class Notificaciones : ContentPage
    {
        public ObservableCollection<Notificacion> NotificacionesList { get; set; }
        HttpClient httpClient { get; set; }
        int CurrentUserId;

        string URL = "https://67e7-2800-e2-407f-fd96-4daa-3067-13f5-605c.ngrok-free.app";

        public Notificaciones()
        {
            InitializeComponent();
            NotificacionesList = new ObservableCollection<Notificacion>();
            httpClient = new HttpClient();
            BindingContext = this;
            CurrentUserId = Preferences.Get("IdUsuario", 0);

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
                var response = await httpClient.GetAsync($"{URL}/notificacion/{CurrentUserId}");
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
