using GamesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GamesApi.DB
{
    public class GameContext: DbContext
    {
        public GameContext(DbContextOptions<GameContext> options): base(options){}
        
        public DbSet<Game> Games { get; set; }
        public DbSet<StudioDeveloper> Developers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudioDeveloper>()
                .HasMany(dev => dev.Games)
                .WithOne(dev => dev.StudioDeveloper)
                .HasForeignKey(g => g.StudioDeveloperId);
        }
    }
}
