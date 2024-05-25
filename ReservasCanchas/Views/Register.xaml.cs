using System.Diagnostics;
using System.Text;
using CommunityToolkit.Maui.Alerts;
using Newtonsoft.Json;
using ReservasCanchas.Models;

namespace ReservasCanchas.Views;

public partial class Register : ContentPage
{
    private readonly HttpClient _httpClient;

    public Register()
    {
        InitializeComponent();
        _httpClient = new HttpClient();

        var tapGestureRecognizer = new TapGestureRecognizer();
        tapGestureRecognizer.Tapped += toLoginPage;
        gotolog.GestureRecognizers.Add(tapGestureRecognizer);
    }

    private async void toLoginPage(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(Login));
    }

    public class RegisterResponse
    {
        public string message { get; set; }
    }

    public async Task<RegisterResponse> RegisterFn(RegisterCredentials credentials)
    {
        boton.Text = "Cargando...";
        try
        {
            var jsonContent = JsonConvert.SerializeObject(credentials);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://reserva-canchas.vercel.app/auth/register", content);

            if (!response.IsSuccessStatusCode)
            {
                await DisplayAlert("Error", "Correo ya en uso", "OK");
                return null;
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(responseContent);

            var data = JsonConvert.DeserializeObject<RegisterResponse>(responseContent);

            if (data != null)
            {
                await Shell.Current.GoToAsync(nameof(Login));
                return data;
            }
            else
            {
                await DisplayAlert("Error", "Correo ya registrado", "OK");
                return null;
            }
        }
        catch (HttpRequestException)
        {
            await DisplayAlert("Error", "Credenciales no validas, intentalo de nuevo", "OK");
        }
        catch (JsonException ex)
        {
            await DisplayAlert("Error", $"Error al procesar la respuesta del servidor: {ex.Message}", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Error: {ex.Message}. Por favor, intenta de nuevo en unos instantes", "OK");
        }
        finally
        {
            boton.Text = "Registrarme";
        }

        return null;
    }

    private async void OnRegisterButtonClicked(object sender, EventArgs e)
    {
        if (usernameEntry == null || passwordEntry == null)
        {
            await DisplayAlert("Error", "Por favor, asegúrate de que los campos de email y contraseña estén definidos.", "OK");
            return;
        }

        var credentials = new RegisterCredentials
        {
            CorreoElectronico = usernameEntry.Text,
            Contrase_a = passwordEntry.Text,
            Apellido = apellidoEntry.Text,
            Nombre = nombreEntry.Text,
            Rol = "User",
            Telefono = tlfEntry.Text
        };

        var user = await RegisterFn(credentials);

        if (user != null)
        {
            await Toast.Make("Registrado con éxito").Show();
        }
    }
}
