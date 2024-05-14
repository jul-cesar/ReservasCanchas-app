namespace ReservasCanchas
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(DetallesCancha), typeof(DetallesCancha));

        }
    }
}
