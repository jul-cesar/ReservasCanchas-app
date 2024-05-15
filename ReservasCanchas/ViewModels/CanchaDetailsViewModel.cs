using System.Diagnostics;
using CommunityToolkit.Maui.Views;
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
        private async Task OpenDetailPopup()
        {
            Debug.WriteLine("work");

            if (cancha != null)
            {
                var detailPopup = new AddReservaView(new ReservaViewModel(serviceReservas, Cancha));
                await Application.Current.MainPage.ShowPopupAsync(detailPopup);
            }
        }


    }

}
