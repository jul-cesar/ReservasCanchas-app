using System.Diagnostics;
using System.Text.Json;
using CommunityToolkit.Maui.Alerts;
using ReservasCanchas.Models;

namespace ReservasCanchas.Services
{
    public class ReservasService
    {
        private readonly HttpClient httpClient;
        List<Suministro> suministrosLista = new();
        List<ReservasResponse> reservasUserLista = new();

        List<ReservasResponse> reservasLista = new();

        public ReservasService()
        {
            httpClient = new HttpClient();
        }

        private const string URL = "https://reserva-canchas-three.vercel.app/reserva";

        public async Task<List<Suministro>> GetSuministros()
        {

            try
            {
                var response = await httpClient.GetAsync("https://reserva-canchas-three.vercel.app/suministro");
                if (response.IsSuccessStatusCode)
                {
                    var suministrosData = await response.Content.ReadAsStringAsync();
                    suministrosLista = JsonSerializer.Deserialize<List<Suministro>>(suministrosData);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Error: {ex.Message}. Por favor, intenta de nuevo en unos instantes", "OK");
            }

            return suministrosLista;
        }

        public async Task<List<ReservasResponse>> GetReservasUser(int IdUser)
        {

            try
            {
                var response = await httpClient.GetAsync($"https://reserva-canchas-three.vercel.app/reserva/user/{IdUser}");
                if (response.IsSuccessStatusCode)
                {
                    var reservasData = await response.Content.ReadAsStringAsync();
                    reservasUserLista = JsonSerializer.Deserialize<List<ReservasResponse>>(reservasData);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Error: {ex.Message}. Por favor, intenta de nuevo en unos instantes", "OK");
            }

            return reservasUserLista;
        }





        public async Task CrearReserva(HttpContent content)
        {
            try
            {
                Debug.WriteLine(content);
                var response = await httpClient.PostAsync(URL, content);

                if (response.IsSuccessStatusCode)
                {

                    await Toast.Make("Reserva hecha con éxito").Show();
                    await Shell.Current.GoToAsync("..");

                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    await Shell.Current.DisplayAlert("Error", $"Error al realizar la reserva: {response.StatusCode}. Mensaje: {errorMessage}", "OK");
                }
            }
            catch (HttpRequestException ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Error de red: {ex.Message}", "OK");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $": {ex.Message}, Por favor, intenta de nuevo en unos instantes", "OK");
            }
        }
    }
}
