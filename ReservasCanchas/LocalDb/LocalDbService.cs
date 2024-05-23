using ReservasCanchas.Models;
using SQLite;

namespace ReservasCanchas.LocalDb
{


    public class LocalDbService
    {
        private const string DBNAME = "favorites.db3";
        private readonly SQLiteAsyncConnection _connection;
        public LocalDbService()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DBNAME));
            _connection.CreateTableAsync<Favorites>();
        }

        public Task<List<Favorites>> GetFavoritosAsync(int? userId)
        {

            return _connection.Table<Favorites>().Where(f => f.IDUser == userId).ToListAsync();
        }

        public Task<int> SaveFavoritoAsync(Favorites favorito)
        {

            return _connection.InsertAsync(favorito);
        }

        public async Task<Favorites> GetFavoritoByCanchaId(int canchaId, int? usuarioId)
        {
            return await _connection.Table<Favorites>().Where(f => f.IdCancha == canchaId && f.IDUser == usuarioId).FirstOrDefaultAsync();
        }

        public async Task DeleteFavoritoAsync(int canchaId, int? usuarioId)
        {
            var favorite = await _connection.Table<Favorites>().Where(f => f.IdCancha == canchaId && f.IDUser == usuarioId).FirstOrDefaultAsync();
            if (favorite != null)
            {
                await _connection.DeleteAsync(favorite);
            }
        }

    }


}
