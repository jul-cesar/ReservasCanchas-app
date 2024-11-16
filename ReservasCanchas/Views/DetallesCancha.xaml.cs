using ReservasCanchas.ViewModels;

namespace ReservasCanchas;

public partial class DetallesCancha : ContentPage
{

    public DetallesCancha(CanchasViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is CanchasViewModel viewModel)
        {
            if (int.IsPositive(viewModel.Cancha.IDCancha))
            {
                viewModel.GetCommentsCanchaCommand.Execute(null);
            }
        }
    }


}