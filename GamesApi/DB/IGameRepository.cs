using GamesApi.Models;

namespace GamesApi.DB
{
    public interface IGameRepository: ICRUDRepository<Game>
    {
        Task<PageList<Game>> GetGamesByGenreAsync(
            GameGenre genre, int currentPage, int pageSize);

        Task<PageList<Game>> GetGamesByDeveloperAsync(
            Guid developerId, int currentPage, int pageSize);

        Task<PageList<Game>> GetGamesByGenreAndDeveloperAsync(
            GameGenre genre, Guid developerId, int currentPage, int pageSize);
    }
}
