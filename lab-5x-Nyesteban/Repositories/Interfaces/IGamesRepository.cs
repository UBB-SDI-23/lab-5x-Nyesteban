using lab_1_Nyesteban.Models;
using Microsoft.AspNetCore.Mvc;

namespace lab_1_Nyesteban.Repositories.Interfaces
{
    public interface IGamesRepository
    {
        public Task<IEnumerable<Game>> GetGames();
        public Task<ActionResult<Game>> GetGame(int id);
        public Task<IEnumerable<Game>> GetGamesPaginated(int skip, int take);
        public Task<Game> PutGame(int id, Game game);
        public Task PostGame(Game game);
        public Task PostGameToOneToMany(int id, Game game);
        public Task DeleteGame(int id);
    }
}
