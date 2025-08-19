namespace Walks.API.Models.Domain
{
    public class Difficulty
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        //navigation properties
        public ICollection<Walk> Walks = new List<Walk>();
    }
}
