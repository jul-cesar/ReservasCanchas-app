using CommunityToolkit.Maui.Views;
using ReservasCanchas.ViewModels;
using UraniumUI.Dialogs;

namespace ReservasCanchas.Views;

public partial class AddReservaView : Popup
{
    public IDialogService DialogService { get; }

    public AddReservaView(ReservaViewModel viewModel, IDialogService dialogService)
    {
        InitializeComponent();
        BindingContext = viewModel;
        DialogService = dialogService;




    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await DialogService.DisplayCheckBoxPromptAsync("Title", new[] { "Option 1", "Option 2", "Option 3" });

    }
}