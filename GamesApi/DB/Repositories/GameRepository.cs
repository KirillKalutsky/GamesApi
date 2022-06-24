using GamesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GamesApi.DB.Repositories
{
    public abstract class GameRepository : Repository<Game>
    {
        public GameRepository(DbContext gameContext) 
            : base(gameContext) { }

        public abstract Task<PageList<Game>> GetGamesByGenreAsync(
            GameGenre genre, int currentPage, int pageSize);

        public abstract Task<PageList<Game>> GetGamesByDeveloperAsync(
            Guid developerId, int currentPage, int pageSize);

        public abstract Task<PageList<Game>> GetGamesByGenreAndDeveloperAsync(
            GameGenre genre, Guid developerId, int currentPage, int pageSize);
    }
}
