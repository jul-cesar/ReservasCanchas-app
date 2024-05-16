namespace ReservasCanchas.Models
{
    public class Suministro
    {



        public int IDSuministro { get; set; }
        public int? IDReserva { get; set; }
        public string TipoSuministro { get; set; }
        public int Cantidad { get; set; }
        public string CostoUnitario { get; set; }


    }
}
