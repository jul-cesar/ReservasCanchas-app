using System.Diagnostics;
using System.Text;
using CommunityToolkit.Maui.Alerts;
using Newtonsoft.Json;
using ReservasCanchas.Models;

namespace ReservasCanchas.Views;

public partial class Login : ContentPage
{
    private readonly HttpClient _httpClient;

    public Login()
    {
        InitializeComponent();
        usernameEntry.Text = "";
        passwordEntry.Text = "";
        _httpClient = new HttpClient();
        var tapGestureRecognizer = new TapGestureRecognizer();
        tapGestureRecognizer.Tapped += toRegisterPage;
        gotoreg.GestureRecognizers.Add(tapGestureRecognizer);
    }

    public async void toRegisterPage(object sender, EventArgs e)
    {
        Application.Current.MainPage = new AppShell();

        await Shell.Current.GoToAsync(nameof(Register), true);
    }

    public async Task<Usuario> LoginFn(Auth credentials)
    {
        boton.Text = "Cargando...";
        try
        {

            var jsonContent = JsonConvert.SerializeObject(credentials);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://67e7-2800-e2-407f-fd96-4daa-3067-13f5-605c.ngrok-free.app/auth/login", content);

            if (!response.IsSuccessStatusCode)
            {
                await DisplayAlert("Error", "Credenciales no validas, intentalo de nuevo", "OK");
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
            boton.Text = "Iniciar";
        }

        return null;
    }



    private async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        if (usernameEntry == null || passwordEntry == null)
        {
            await DisplayAlert("Error", "Por favor, aseg�rate de que los campos de email y contrase�a est�n definidos.", "OK");
            return;
        }

        var credentials = new Auth
        {
            email = usernameEntry.Text.ToLower(),
            password = passwordEntry.Text.ToLower()
        };

        var user = await LoginFn(credentials);

        if (user != null)
        {
            await Toast.Make("Sesion iniciada correctamente").Show();

        }
    }
}
