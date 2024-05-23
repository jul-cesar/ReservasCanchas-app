using System.ComponentModel;

namespace ReservasCanchas.Models
{
    public class Cancha : INotifyPropertyChanged
    {
        private bool isFavorite;

        public int IDCancha { get; set; }
        public string Nombre { get; set; }
        public string ImgURL { get; set; }
        public string Direccion { get; set; }
        public string HoraApertura { get; set; }
        public string Dimensiones { get; set; }
        public string Contacto { get; set; }
        public string HoraCierre { get; set; }
        public string Descripcion { get; set; }
        public int CapacidadMaxima { get; set; }
        public string TipoSuperficie { get; set; }
        public string Disponibilidad { get; set; }
        public string Precio { get; set; }

        public bool IsFavorite
        {
            get => isFavorite;
            set
            {
                if (isFavorite != value)
                {
                    isFavorite = value;
                    OnPropertyChanged(nameof(IsFavorite));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
