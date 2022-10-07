using System.ComponentModel.DataAnnotations;

namespace GamesApi.Models.Dto
{
    public class UpdateGameDto
    {
        public Guid? StudioDeveloperId { get; set; }
        public string? Name { get; set; }
        public IEnumerable<GameGenre>? GameGenres { get; set; }

    }
}
