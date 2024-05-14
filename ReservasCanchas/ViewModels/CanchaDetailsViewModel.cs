using System.Diagnostics;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReservasCanchas.Models;
using ReservasCanchas.Services;
using ReservasCanchas.Views;

namespace ReservasCanchas.ViewModels
{
    [QueryProperty("Cancha", "Cancha")]
    public partial class CanchaDetailsViewModel : BaseViewModel
    {
        private ReservasService serviceReservas;

        public CanchaDetailsViewModel(ReservasService service)
        {
            serviceReservas = service;

        }
        [ObservableProperty]
        private Cancha selectedCancha;

        [ObservableProperty]
        Cancha cancha;


        [RelayCommand]
        private async Task OpenDetailPopup()
        {
            Debug.WriteLine("work");
            SelectedCancha = Cancha;
            if (SelectedCancha != null)
            {
                var detailPopup = new AddReservaView(new ReservaViewModel(serviceReservas, SelectedCancha));
                await Application.Current.MainPage.ShowPopupAsync(detailPopup);
            }
        }


    }

}
