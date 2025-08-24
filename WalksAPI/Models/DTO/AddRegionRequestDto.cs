using System.ComponentModel.DataAnnotations;

namespace Walks.API.Models.DTO
{
    public class AddRegionRequestDto
    {
        [Required]
        [Length(3,3,ErrorMessage = "Code has to be of length 3")]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
