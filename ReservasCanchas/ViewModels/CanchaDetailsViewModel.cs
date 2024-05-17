using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReservasCanchas.Models;
using ReservasCanchas.Services;
using ReservasCanchas.Views;
using UraniumUI.Dialogs;

namespace ReservasCanchas.ViewModels
{
    [QueryProperty("Cancha", "Cancha")]
    public partial class CanchaDetailsViewModel : BaseViewModel
    {
        private ReservasService serviceReservas;
        public IDialogService DialogService { get; }

        public CanchaDetailsViewModel(ReservasService service, IDialogService dialogService)
        {
            serviceReservas = service;
            DialogService = dialogService;

        }


        [ObservableProperty]
        Cancha cancha;


        [RelayCommand]
        private async Task OpenDetailPopup(Cancha cancha)
        {
            Debug.WriteLine("work");


            await Shell.Current.GoToAsync($"{nameof(AddReservaView)}", true, new Dictionary<string, object> { { "Cancha", cancha } });

        }

        [RelayCommand]
        private async Task goToReservasCancha(Cancha cancha)
        {
            await Shell.Current.GoToAsync($"{nameof(ReservasCancha)}", true, new Dictionary<string, object> { { "Cancha", cancha } });

        }


    }

}


