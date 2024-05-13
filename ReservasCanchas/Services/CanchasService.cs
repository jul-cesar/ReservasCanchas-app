using System.Net.Http.Json;
using ReservasCanchas.Models;

namespace ReservasCanchas.Services
{
    public class CanchasService
    {
        HttpClient httpClient { get; set; }

        string URL = "https://reservas-canchas.onrender.com";

        List<Cancha> canchasLista = new();

        public CanchasService()
        {

            httpClient = new HttpClient();
        }

        public async Task<List<Cancha>> getCanchas()
        {
            if (canchasLista.Count > 0)
            {
                return canchasLista;
            }

            var response = await httpClient.GetAsync($"{URL}/cancha");
            if (response.IsSuccessStatusCode)
            {
                canchasLista = await response.Content.ReadFromJsonAsync<List<Cancha>>();
            }

            return canchasLista;

        }
    }
}
