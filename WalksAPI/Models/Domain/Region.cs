namespace Walks.API.Models.Domain
{
    public class Region
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }

        //navigation properties
        public ICollection<Walk> Walks=new List<Walk>();
    }
}
