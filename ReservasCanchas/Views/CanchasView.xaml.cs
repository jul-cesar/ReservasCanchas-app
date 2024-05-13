using ReservasCanchas.ViewModels;

namespace ReservasCanchas.Views;

public partial class CanchasView : ContentPage
{


    public CanchasView(CanchasViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }


}