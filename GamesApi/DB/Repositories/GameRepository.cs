using GamesApi.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace GamesApi.DB.Repositories
{
    public class GameRepository : Repository<Game>, IGameRepository
    {
        private readonly IMapper mapper;
        public GameRepository(GameContext context, IMapper mapper) : base(context)
        {
            this.mapper = mapper;
        }

        public override Task<PageList<Game>> GetAsync(int currentPage, int pageSize)
        {
            IQueryable<Game> gamesList = set
                //.Include(game => game.StudioDeveloper)
                .OrderBy(game => game.Name);
            var list = new PageList<Game>(gamesList, currentPage, pageSize);
            return Task.FromResult(list);
        }

        public Task<PageList<Game>> GetGamesByDeveloperAsync(Guid developerId, int currentPage, int pageSize)
        {
            IQueryable<Game> games = set
                .Where(game => game.StudioDeveloperId == developerId);

            return Task.FromResult(new PageList<Game>(games, currentPage, pageSize));
        }

        public Task<PageList<Game>> GetGamesByGenreAndDeveloperAsync(GameGenre genre, Guid developerId, int currentPage, int pageSize)
        {
            IQueryable<Game> games = set
                .Where(game =>
                    game.StudioDeveloperId == developerId &&
                    game.GameGenres.Contains(genre));

            return  Task.FromResult(new PageList<Game>(games, currentPage, pageSize));
        }

        public Task<PageList<Game>> GetGamesByGenreAsync(GameGenre genre, int currentPage, int pageSize)
        {
            IQueryable<Game> gamesList = set
                //.Include(game => game.StudioDeveloper)
                .Where(game => game.GameGenres.Contains(genre))
                .OrderBy(game => game.Name);
            var list = new PageList<Game>(gamesList, currentPage, pageSize);
            return Task.FromResult(list);
        }

        public async override Task UpdateAsync(Game obj)
        {
            var old = await GetAsync(obj.Id);

            if (old == null)
                await InsertAsync(obj);
            else
                mapper.Map(obj,old);

            await SaveAsync();
        }
    }
}
