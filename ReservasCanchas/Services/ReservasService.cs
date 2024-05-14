using System.Text;
using System.Text.Json;
using ReservasCanchas.Models;

namespace ReservasCanchas.Services
{
    public class ReservasService
    {
        HttpClient httpClient { get; set; }
        string URL = "https://reservas-canchas.onrender.com/reserva";

        public async Task crearReserva(Reserva reserva)
        {
            var jsonReserva = JsonSerializer.Serialize(reserva);
            var content = new StringContent(jsonReserva, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"{URL}/cancha", content);
            if (response.IsSuccessStatusCode)
            {

                await Shell.Current.DisplayAlert("Exito", "Reserva hecha con exito", "ok");
            }

        }

    }
}
