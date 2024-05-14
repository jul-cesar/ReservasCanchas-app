namespace ReservasCanchas.Services
{
    public class ReservasService
    {
        HttpClient httpClient { get; set; }
        string URL = "https://reservas-canchas.onrender.com/reserva";

        public async Task crearReserva()
        {

            var response = await httpClient.PostAsync($"{URL}/cancha", { });
            if (response.IsSuccessStatusCode)
            {
                var productData = await response.Content.ReadAsStringAsync();
                canchasLista = JsonSerializer.Deserialize<List<Cancha>>(productData);

            }

            return canchasLista;

        }

    }
}
