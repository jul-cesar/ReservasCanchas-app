using System.Text.Json;
using ReservasCanchas.Models;

namespace ReservasCanchas.Services
{
    public class CanchasService
    {
        HttpClient httpClient { get; set; }

        string URL = "https://reserva-canchas-three.vercel.app";

        List<Cancha> canchasLista = new();

        public CanchasService()
        {

            httpClient = new HttpClient();
        }

        public async Task<List<Cancha>> getCanchas()
        {

            var response = await httpClient.GetAsync($"{URL}/cancha");
            if (response.IsSuccessStatusCode)
            {
                var productData = await response.Content.ReadAsStringAsync();
                canchasLista = JsonSerializer.Deserialize<List<Cancha>>(productData);

            }

            return canchasLista;

        }
    }
}
