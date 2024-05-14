using ReservasCanchas.ViewModels;

namespace ReservasCanchas.Views;

public partial class ReservasView : ContentPage
{
    public ReservasView(ReservaViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;

    }
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}