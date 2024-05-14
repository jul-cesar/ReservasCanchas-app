using CommunityToolkit.Maui.Views;
using ReservasCanchas.ViewModels;

namespace ReservasCanchas.Views;

public partial class AddReservaView : Popup
{

    public AddReservaView(ReservaViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;

    }
}