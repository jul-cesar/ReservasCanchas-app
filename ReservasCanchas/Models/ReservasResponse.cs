namespace ReservasCanchas.Models
{
    public class ReservasResponse
    {
        public int IDReserva { get; set; }
        public int IDUsuario { get; set; }
        public int IDCancha { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaReserva { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFinalizacion { get; set; }

        public int Duracion { get; set; }
        public string Estado { get; set; }
        public string MetodoPago { get; set; }
        public string MontoPagado { get; set; }
        public Cancha canchas { get; set; }
        public List<Factura> facturas { get; set; }
        public List<object> suministrosadicionales { get; set; }
        public Usuario usuarios { get; set; }
    }
}
