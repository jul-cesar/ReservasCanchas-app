namespace ReservasCanchas.Models
{
    public class Notificacion
    {


        public int IDNotificacion { get; set; }
        public int IDUsuario { get; set; }
        public string Tipo { get; set; }
        public string Mensaje { get; set; }
        public DateTime FechaHoraEnvio { get; set; }


    }
}
