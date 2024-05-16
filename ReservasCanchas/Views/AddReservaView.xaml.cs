using ReservasCanchas.ViewModels;

namespace ReservasCanchas.Views;

public partial class AddReservaView : ContentPage
{
    private readonly ReservaViewModel _viewModel;

    public AddReservaView(ReservaViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;



    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.GetSuministrosAsync();
    }



}