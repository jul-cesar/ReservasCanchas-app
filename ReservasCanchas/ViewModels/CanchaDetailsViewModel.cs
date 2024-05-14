using CommunityToolkit.Mvvm.ComponentModel;
using ReservasCanchas.Models;

namespace ReservasCanchas.ViewModels
{
    [QueryProperty("Cancha", "Cancha")]
    public partial class CanchaDetailsViewModel : BaseViewModel
    {

        public CanchaDetailsViewModel()
        {

        }
        [ObservableProperty]
        Cancha cancha;



    }

}
