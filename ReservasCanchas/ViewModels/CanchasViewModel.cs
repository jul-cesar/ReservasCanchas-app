
using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReservasCanchas.LocalDb;
using ReservasCanchas.Models;
using ReservasCanchas.Services;
using ReservasCanchas.Views;
namespace ReservasCanchas.ViewModels
{
    [QueryProperty(nameof(Cancha), "Cancha")]

    public partial class CanchasViewModel : BaseViewModel

    {
        CanchasService canchasService;
        IConnectivity connectivity;
        LocalDbService localDb;
        [ObservableProperty]
        public ObservableCollection<Cancha> canchas = new();

        [ObservableProperty]
        public ObservableCollection<Comentario> comments = new();

        public CanchasViewModel(CanchasService canchasService, IConnectivity connectivity, LocalDbService dbl)

        {
            Title = "Lista canchas";
            this.canchasService = canchasService;
            this.connectivity = connectivity;
            Nombre = Preferences.Get("Nombre", string.Empty);
            CurrentUserId = Preferences.Get("IdUsuario", 0);
            this.localDb = dbl;

        }
        [ObservableProperty]
        bool isRefreshing;

        [ObservableProperty]
        Cancha cancha;

        [ObservableProperty]

        string nombre;

        [ObservableProperty]

        int currentUserId;

        [ObservableProperty]
        private string comentarioContenido;

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
        async Task ToggleFavoriteAsync(Cancha cancha)
        {
            if (cancha is null)
            {
                return;
            }

            try
            {
                if (cancha.IsFavorite)
                {
                    cancha.IsFavorite = false;
                    await localDb.DeleteFavoritoAsync(cancha.IDCancha, currentUserId);
                    await Shell.Current.DisplayAlert("Eliminado", "Eliminado de favoritos", "OK");
                }
                else
                {
                    cancha.IsFavorite = true;
                    var favorito = new Favorites { IDUser = CurrentUserId, IdCancha = cancha.IDCancha };

                    await localDb.SaveFavoritoAsync(favorito);
                    await Shell.Current.DisplayAlert("Agregado", "Agregado a favoritos", "OK");

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error toggling favorite: {ex.Message}");
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
                    return;
                }
                if (!IsRefreshing)
                {
                    IsBusy = true;
                }

                var canchas = await canchasService.GetCanchasAsync();
                var favoriteCanchas = await localDb.GetFavoritosAsync(currentUserId);

                if (Canchas.Count != 0)
                {
                    Canchas.Clear();
                }
                foreach (var cancha in canchas)
                {
                    cancha.IsFavorite = favoriteCanchas.Any(f => f.IdCancha == cancha.IDCancha);
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

        [RelayCommand]
        async Task LogOut()
        {
            try
            {

                Preferences.Remove("IdUsuario");
                Preferences.Remove("rol");
                Preferences.Remove("Nombre");

                await Shell.Current.GoToAsync(nameof(Login), true);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

            }
        }

        [RelayCommand]
        private async Task OpenDetailPopup()
        {
            Debug.WriteLine("work");


            await Shell.Current.GoToAsync($"{nameof(AddReservaView)}", true, new Dictionary<string, object> { { "Cancha", Cancha } });

        }

        [RelayCommand]
        private async Task goToReservasCancha()
        {
            await Shell.Current.GoToAsync($"{nameof(ReservasCancha)}", true, new Dictionary<string, object> { { "Cancha", Cancha } });

        }

        [RelayCommand]

        public async Task GetCommentsCanchaAsync ()
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
                    return;
                }
                if (!IsRefreshing)
                {
                    IsBusy = true;
                }

                var response  = await canchasService.GetComentariosCanchaAsync(Cancha.IDCancha);

               Comments =  new ObservableCollection<Comentario>(response);

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

        [RelayCommand]

        public async Task CreateComment ()
        {
            if(string.IsNullOrEmpty(ComentarioContenido))
            {
                await Shell.Current.DisplayAlert("error", "No puedes crear un comentario vacio", "Ok");
                return ;
            }
            try
            {
                var newComment = new CrearComentario
                {
                    Contenido = ComentarioContenido,
                    IDCancha = Cancha.IDCancha,
                    IDUsuario = CurrentUserId,
                };

                var response = await canchasService.CrearComentarioService(newComment);
                if(response is not null)
                {
                    ComentarioContenido = string.Empty;
                    await GetCommentsCanchaAsync();
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
        }
 

    }
}
