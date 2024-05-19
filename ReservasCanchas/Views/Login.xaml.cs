using System.Diagnostics;
using System.Text;
using Newtonsoft.Json;
using ReservasCanchas.Models;

namespace ReservasCanchas.Views;

public partial class Login : ContentPage
{
    private readonly HttpClient _httpClient;

    public Login()
    {
        InitializeComponent();
        _httpClient = new HttpClient();
    }

    public async Task<Usuario> LoginFn(Auth credentials)
    {
        try
        {
            var jsonContent = JsonConvert.SerializeObject(credentials);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://reserva-canchas.vercel.app/auth/login", content);

            if (!response.IsSuccessStatusCode)
            {
                await DisplayAlert("Error", $"Error de red: {response.StatusCode}", "OK");
                return null;
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(responseContent);

            var data = JsonConvert.DeserializeObject<Usuario>(responseContent);

            if (data != null)
            {
                Preferences.Set("IdUsuario", data.IDUsuario);
                Preferences.Set("rol", data.Rol);
                Preferences.Set("Nombre", data.Nombre);


                Application.Current.MainPage = new AppShell();
                await Shell.Current.GoToAsync("//MainPage");

                return data;
            }
            else
            {
                await DisplayAlert("Error", "Credenciales inv�lidas.", "OK");
                return null;
            }
        }
        catch (HttpRequestException ex)
        {
            await DisplayAlert("Error", $"Error de red: {ex.Message}", "OK");
        }
        catch (JsonException ex)
        {
            await DisplayAlert("Error", $"Error al procesar la respuesta del servidor: {ex.Message}", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Error: {ex.Message}. Por favor, intenta de nuevo en unos instantes", "OK");
        }

        return null;
    }



    private async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        // Aseg�rate de que las entradas no sean null
        if (usernameEntry == null || passwordEntry == null)
        {
            await DisplayAlert("Error", "Por favor, aseg�rate de que los campos de email y contrase�a est�n definidos.", "OK");
            return;
        }

        var credentials = new Auth
        {
            email = usernameEntry.Text,
            password = passwordEntry.Text
        };

        var user = await LoginFn(credentials);

        if (user != null)
        {
            await DisplayAlert("�xito", "Inicio de sesi�n exitoso", "OK");
        }
    }
}