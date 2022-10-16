using GamesApi.Models;
using AutoMapper;
using GamesApi.DB;
using Microsoft.EntityFrameworkCore;

namespace GamesApi.DB.Repositories
{
    public class DeveloperRepository : Repository<StudioDeveloper>, IDeveloperRepository
    {
        private readonly IMapper mapper;
        public DeveloperRepository(GameContext gameContext, IMapper mapper)
            : base(gameContext)
        {
            this.mapper = mapper;
        }

        public override Task<PageList<StudioDeveloper>> GetAsync(int currentPage, int pageSize)
        {
            IQueryable<StudioDeveloper> developers = set
                .Include(dev => dev.Games)
                .OrderBy(dev => dev.Name);

            return Task.FromResult(new PageList<StudioDeveloper>(developers, currentPage, pageSize));
        }

        public override async Task UpdateAsync(StudioDeveloper obj)
        {
            var developer = await GetAsync(obj.Id);

            if (developer == null)
                await InsertAsync(obj);
            else
                mapper.Map(obj, developer);

            await SaveAsync();
        }
    }
}
