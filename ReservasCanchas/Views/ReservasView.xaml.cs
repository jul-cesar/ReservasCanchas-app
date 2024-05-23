using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using ReservasCanchas.Models;
using ReservasCanchas.Services;

namespace ReservasCanchas.Views
{
    public partial class ReservasView : ContentPage, INotifyPropertyChanged
    {
        private ReservasService service;
        private int CurrentUserId;
        private ObservableCollection<ReservasResponse> _reservasUser;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<ReservasResponse> ReservasUser
        {
            get => _reservasUser;
            set
            {
                _reservasUser = value;
                OnPropertyChanged();
            }
        }

        public ReservasView(ReservasService reservasService)
        {
            InitializeComponent();
            service = reservasService;
            CurrentUserId = Preferences.Get("IdUsuario", 0);
            ReservasUser = new ObservableCollection<ReservasResponse>();

            BindingContext = this;
        }

        public async Task GetReservasUser()
        {
            try
            {
                var reservasUserList = await service.GetReservasUser(CurrentUserId);

                if (reservasUserList == null)
                {
                    Debug.WriteLine("La lista de suministros es nula");
                    return;
                }
                ReservasUser = new ObservableCollection<ReservasResponse>(reservasUserList);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al obtener suministros: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", $"Error al obtener suministros: {ex.Message}", "OK");
            }
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await GetReservasUser();
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
