using ReservasCanchas.ViewModels;

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


}