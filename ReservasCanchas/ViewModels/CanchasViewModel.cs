
using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.Input;
using ReservasCanchas.Models;
using ReservasCanchas.Services;
namespace ReservasCanchas.ViewModels
{
    public partial class CanchasViewModel : BaseViewModel

    {
        CanchasService canchasService;
        public ObservableCollection<Cancha> Canchas { get; } = new();
        public CanchasViewModel(CanchasService canchasService)
        {
            Title = "Lista canchas";
            this.canchasService = canchasService;

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
                IsBusy = true;
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
            }

        }

    }
}
