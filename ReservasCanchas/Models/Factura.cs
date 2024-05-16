namespace ReservasCanchas.Models
{

    public class Factura
    {
        public int IDFactura { get; set; }
        public int IDReserva { get; set; }
        public string MontoTotal { get; set; }
        public DateTime FechaEmision { get; set; }
        public string EstadoPago { get; set; }
    }
}
