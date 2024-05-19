
using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReservasCanchas.Models;
using ReservasCanchas.Services;
namespace ReservasCanchas.ViewModels
{
    public partial class CanchasViewModel : BaseViewModel

    {
        CanchasService canchasService;
        IConnectivity connectivity;
        public ObservableCollection<Cancha> Canchas { get; } = new();
        public CanchasViewModel(CanchasService canchasService, IConnectivity connectivity)
        {
            Title = "Lista canchas";
            this.canchasService = canchasService;
            this.connectivity = connectivity;
            Nombre = Preferences.Get("Nombre", string.Empty);

        }
        [ObservableProperty]
        bool isRefreshing;

        [ObservableProperty]

        string nombre;

        [RelayCommand]
        async Task GoToDetailsAsync(Cancha cancha)
        {
            if (cancha is null)
            {
                return;
            }
            try
            {
                await Shell.Current.GoToAsync($"{nameof(DetallesCancha)}", true, new Dictionary<string, object> { { "Cancha", cancha } });
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Navigation failed: {ex.Message}");
            }

        }


        [RelayCommand]
        async Task getCanchasAsync()
        {
            if (IsBusy)
            {
                return;
            }
            try
            {
                if (connectivity.NetworkAccess != NetworkAccess.Internet)
                {

                    await Shell.Current.DisplayAlert("Internet error", "Revisa tu conexion a internet e intentalo de nuevo", "ok");
                }
                if (!IsRefreshing)
                {
                    IsBusy = true;
                }

                var canchas = await canchasService.getCanchas();

                if (Canchas.Count != 0)
                {
                    Canchas.Clear();
                }
                foreach (var cancha in canchas)
                {
                    Canchas.Add(cancha);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error", "Error trayendo las canchas", "ok");
            }
            finally
            {

                IsBusy = false;
                IsRefreshing = false;
            }

        }

    }
}
