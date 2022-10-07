using System.ComponentModel.DataAnnotations;

namespace GamesApi.Models.Dto
{
    public class StudioDeveloperDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime FoundingDate { get; set; }
    }
}
