using GamesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GamesApi.DB.Repositories
{
    public abstract class DevelopersRepository : Repository<StudioDeveloper>
    {
        public DevelopersRepository(DbContext context) : base(context)
        {}

    }
}
