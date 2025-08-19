using System.ComponentModel.DataAnnotations.Schema;

namespace Walks.API.Models.Domain
{
    public class Walk
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKms { get; set; }
        public string? WalkImageUrl { get; set; }
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }

        // Navigation Properties
        [ForeignKey("DifficultyId")]
        public Difficulty Difficulty { get; set; }

        public Region Region { get; set; }


    }
}
