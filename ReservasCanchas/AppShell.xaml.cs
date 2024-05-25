﻿using ReservasCanchas.Views;

namespace ReservasCanchas
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(DetallesCancha), typeof(DetallesCancha));
            Routing.RegisterRoute(nameof(AddReservaView), typeof(AddReservaView));
            Routing.RegisterRoute(nameof(ReservasCancha), typeof(ReservasCancha));
            Routing.RegisterRoute(nameof(Register), typeof(Register));
            Routing.RegisterRoute(nameof(Login), typeof(Login));



        }
    }
}
