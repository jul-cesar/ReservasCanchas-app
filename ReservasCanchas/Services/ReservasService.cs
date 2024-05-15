namespace ReservasCanchas.Services
{
    public class ReservasService
    {
        private readonly HttpClient httpClient;

        public ReservasService()
        {
            httpClient = new HttpClient();
        }

        private const string URL = "https://reservas-canchas.onrender.com/reserva";

        public async Task CrearReserva(HttpContent content)
        {
            try
            {

                var response = await httpClient.PostAsync(URL, content);

                if (response.IsSuccessStatusCode)
                {
                    await Shell.Current.DisplayAlert("Éxito", "Reserva hecha con éxito", "OK");
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
                await Shell.Current.DisplayAlert("Error", $"Error inesperado: {ex.Message}", "OK");
            }
        }
    }
}
