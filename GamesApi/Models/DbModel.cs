using System.ComponentModel.DataAnnotations;

namespace GamesApi.Models
{
    public class DbModel
    {
        [Key]
        public Guid Id { get; private set; }
        public DbModel()
        {
            Id = Guid.NewGuid();
        }

        public DbModel(Guid id)
        {
            Id = id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        
    }
}
