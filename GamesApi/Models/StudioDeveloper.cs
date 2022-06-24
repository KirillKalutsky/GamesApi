namespace GamesApi.Models
{
    public class StudioDeveloper : DbModel
    {
        public StudioDeveloper(Guid id) : base(id) { }
        public StudioDeveloper() : base() { }
        public string Name { get; set; }
        public virtual IEnumerable<Game> Games { get; set; }
        public DateTime FoundingDate { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null || obj is not StudioDeveloper)
                return false;
            return Id.Equals(((StudioDeveloper)obj).Id);
        }
    }
}
