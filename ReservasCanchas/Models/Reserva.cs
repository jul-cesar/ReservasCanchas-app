namespace ReservasCanchas.Models
{
    public class Reserva
    {


        public int IDUsuario { get; set; }
        public int IDCancha { get; set; }
        public string FechaInicio { get; set; }
        public string HoraInicio { get; set; }
        public int Duracion { get; set; }
        public string Estado { get; set; }
        public string MetodoPago { get; set; }
        public string EstadoPago { get; set; }
        public float MontoPagado { get; set; }
        public int[] suministrosadicionales { get; set; }





    }
}
