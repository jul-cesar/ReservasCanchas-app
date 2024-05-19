using ReservasCanchas.Views;

namespace ReservasCanchas
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new Login();
        }
    }
}
