using System.ComponentModel.DataAnnotations;

namespace Walks.API.Models.DTO
{
    public class UpdateRegionRequestDto
    {
        [Required]
        [MaxLength(3, ErrorMessage = "Code has to be a maximum of 3 characters")]
        [MinLength(3, ErrorMessage = "Code has to be a minimum of 3 characters")]
        public string Code { get; set; }
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
