using System.ComponentModel.DataAnnotations;

namespace GamesApi.Models.Dto
{
    public class GameDto
    {
        [Required]
        public Guid StudioDeveloperId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public IEnumerable<GameGenre> GameGenres { get; set; }

    }
}
