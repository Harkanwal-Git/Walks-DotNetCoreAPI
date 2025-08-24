using Microsoft.EntityFrameworkCore;
using Walks.API.Data;
using Walks.API.Models.Domain;
using Walks.API.Models.DTO;

namespace Walks.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly WalksDBContext dBContext;
        public SQLRegionRepository(WalksDBContext dBContext) { this.dBContext = dBContext; }
        public async Task<Region> CreateRegionAsync(Region regionDomainModel)
        {
            //Region region = new Region();
            //region.Id = Guid.NewGuid();
            //region.Code = addRegionRequestDto.Code;
            //region.Name = addRegionRequestDto.Name;
            //region.RegionImageUrl = addRegionRequestDto.RegionImageUrl;

            await dBContext.Regions.AddAsync(regionDomainModel);
            await dBContext.SaveChangesAsync();

            return regionDomainModel;
        }

        public async Task<Region?> DeleteRegionAsync(Guid id)
        {
            var region=await dBContext.Regions.FirstOrDefaultAsync(r => r.Id == id);
            if(region == null)
            {
                return null;
            }
            dBContext.Regions.Remove(region);
           await dBContext.SaveChangesAsync();
            return region;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await dBContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetRegionByIdAsync(Guid id)
        {
            return await dBContext.Regions.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Region?> UpdateRegionAsync(Guid id,Region regionDomainModel)
        {
            var region=await dBContext.Regions.FirstOrDefaultAsync(r=>r.Id == id);

            if(region == null)
            {
                return null;
            }
            region.Code=regionDomainModel.Code;
            region.Name= regionDomainModel.Name;
            region.RegionImageUrl=regionDomainModel.RegionImageUrl;

            await dBContext.SaveChangesAsync();
            return region;
        }
    }
}
