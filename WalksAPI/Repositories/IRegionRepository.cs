using Walks.API.Models.Domain;
using Walks.API.Models.DTO;

namespace Walks.API.Repositories
{
    public interface IRegionRepository
    {
        public Task<List<Region>> GetAllAsync();
        public Task<Region?> GetRegionByIdAsync(Guid id);
        public Task<Region> CreateRegionAsync(Region regionDomainModel);
        public Task<Region?> UpdateRegionAsync(Guid id,Region regionDomainModel);
        public Task<Region?> DeleteRegionAsync(Guid id);

    }
}
