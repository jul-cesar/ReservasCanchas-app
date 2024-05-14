namespace ReservasCanchas.Models
{
    public class Reserva
    {




        public int IDUsuario { get; set; }
        public int IDCancha { get; set; }
        public DateTime HoraInicio { get; set; }
        public int Duracion { get; set; }
        public int MontoPagadl { get; set; }
        public string Estado { get; set; }
        public int MontoPagado { get; set; }
        public string MetodoPago { get; set; }
        public int[] suministrosadicionales { get; set; }



    }
}
