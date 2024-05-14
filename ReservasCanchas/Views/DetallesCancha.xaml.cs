using CommunityToolkit.Maui.Views;
using ReservasCanchas.ViewModels;
using ReservasCanchas.Views;

namespace ReservasCanchas;

public partial class DetallesCancha : ContentPage
{

    public DetallesCancha(CanchaDetailsViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;

    }
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {

        if (BindingContext is CanchaDetailsViewModel viewModel)
        {

            await this.ShowPopupAsync(new AddReservaView(viewModel.Cancha));
        }


    }
}