using System.ComponentModel.DataAnnotations;

namespace GamesApi.Models.Dto
{
    public class UpdateStudioDeveloperDto
    {
        public string? Name { get; set; }
        public DateTime? FoundingDate { get; set; }
    }
}
