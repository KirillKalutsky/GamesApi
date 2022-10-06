using System.ComponentModel.DataAnnotations;

namespace GamesApi.Models.Dto
{
    public class GameDto
    {
        public Guid? StudioDeveloperId { get; set; }
        public string? Name { get; set; }
        public IEnumerable<GameGenre>? GameGenres { get; set; }

    }
}
