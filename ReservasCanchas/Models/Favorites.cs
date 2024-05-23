
using SQLite;

namespace ReservasCanchas.Models
{
    [Table("Favorites")]
    public class Favorites
    {
        [PrimaryKey]
        [AutoIncrement]

        public int Id { get; set; }
        public int IdCancha { get; set; }
        public int IDUser { get; set; }
    }
}
