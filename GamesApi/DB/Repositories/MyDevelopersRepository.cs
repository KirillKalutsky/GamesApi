using GamesApi.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace GamesApi.DB.Repositories
{
    public class MyDevelopersRepository : DevelopersRepository
    {
        private readonly IMapper mapper;
        public MyDevelopersRepository(GameContext gameContext, IMapper mapper)
            : base(gameContext)
        {
            this.mapper = mapper;
        }

        public override async Task<PageList<StudioDeveloper>> GetAllAsync(int currentPage, int pageSize)
        {
            return await Task.Run(() => {
                IQueryable<StudioDeveloper> developers = set
                    .OrderBy(dev => dev.Name);

                return new PageList<StudioDeveloper>(developers, currentPage, pageSize);
            });
        }

        public override async Task UpdateAsync(StudioDeveloper obj)
        {
            var developer = await ReadAsync(obj.Id);

            if (developer == null)
                await CreateAsync(obj);
            else
                mapper.Map(obj, developer);

            await SaveAsync();
        }
    }
}
