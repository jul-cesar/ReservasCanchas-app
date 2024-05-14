using CommunityToolkit.Maui.Views;
using ReservasCanchas.Models;

namespace ReservasCanchas.Views;

public partial class AddReservaView : Popup
{
    Cancha canchaReserva;
    public AddReservaView(Cancha cancha)
    {
        InitializeComponent();
        this.canchaReserva = cancha;
        this.BindingContext = cancha;
    }
}