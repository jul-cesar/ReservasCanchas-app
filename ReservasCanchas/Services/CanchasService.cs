using System.Text;
using Newtonsoft.Json;
using ReservasCanchas.Models;

namespace ReservasCanchas.Services
{
    public class CanchasService
    {
        private readonly HttpClient httpClient;
        private readonly string URL = "https://67e7-2800-e2-407f-fd96-4daa-3067-13f5-605c.ngrok-free.app";

        public CanchasService()
        {
            httpClient = new HttpClient();
        }

        public async Task<List<Cancha>> GetCanchasAsync()
        {

            var response = await httpClient.GetAsync($"{URL}/cancha");
            if (response.IsSuccessStatusCode)
            {
                var productData = await response.Content.ReadAsStringAsync();
                var dataResponse = JsonConvert.DeserializeObject<List<Cancha>>(productData);
                return dataResponse ?? new List<Cancha>();
            }
            else
            {
                Console.WriteLine($"Error: Unable to fetch canchas. Status Code: {response.StatusCode}");
                return new List<Cancha>();
            }

        }

        public async Task<List<Comentario>> GetComentariosCanchaAsync(int idCancha)
        {

            var response = await httpClient.GetAsync($"{URL}/comments/{idCancha}");
            if (response.IsSuccessStatusCode)
            {
                var productData = await response.Content.ReadAsStringAsync();
                var dataResponse = JsonConvert.DeserializeObject<List<Comentario>>(productData);
                Console.WriteLine(JsonConvert.SerializeObject(dataResponse, Formatting.Indented));

                return dataResponse ?? new List<Comentario>();
            }
            else
            {
                Console.WriteLine($"Error: Unable to fetch comments for Cancha {idCancha}. Status Code: {response.StatusCode}");
                return new List<Comentario>();
            }
        }

        public async Task<HttpResponseMessage> CrearComentarioService(CrearComentario data)
        {
            try
            {
                var jsonContent = JsonConvert.SerializeObject(data);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync($"{URL}/comments", content);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error: Unable to create comment. Status Code: {response.StatusCode}, Details: {errorContent}");

                    throw new Exception($"Error al crear comentario: {response.StatusCode}");
                }

                return response;
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($"Network error: {httpEx.Message}");
                throw new Exception("Error de red al intentar crear el comentario. Por favor, revisa tu conexión a Internet.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                throw new Exception("Ocurrió un error inesperado al intentar crear el comentario.");
            }
        }
    }

    }

