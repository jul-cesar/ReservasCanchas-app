using ReservasCanchas.ViewModels;

namespace ReservasCanchas.Views;

public partial class CanchasView : ContentPage
{
    public CanchasView(CanchasViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is CanchasViewModel viewModel)
        {
            viewModel.getCanchasCommand.Execute(null);
        }
    }
}
