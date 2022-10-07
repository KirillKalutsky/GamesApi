namespace GamesApi.Models
{
    public class Game : DbModel
    {
        public Game(Guid id) : base(id) { }
        public Game() : base() { }
        public string? Name { get; set; }
        public GameGenre[]? GameGenres { get; set; }
        public Guid? StudioDeveloperId { get; set; }
        public virtual StudioDeveloper? StudioDeveloper { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null || obj is not Game)
                return false;
            return Id.Equals(((Game)obj).Id);
        }
    }
}
