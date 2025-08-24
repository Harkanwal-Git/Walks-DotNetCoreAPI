using Microsoft.EntityFrameworkCore;
using Walks.API.Data;
using Walks.API.Models.Domain;
using Walks.API.Models.DTO;

namespace Walks.API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly WalksDBContext dBContext;
        public SQLWalkRepository(WalksDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dBContext.Walks.AddAsync(walk);
            await dBContext.SaveChangesAsync();
            return walk;
        }

        public async Task<List<Walk>> GetAllAsync()
        {
            return await dBContext.Walks.Include(x=>x.Difficulty).Include("Region").ToListAsync();
            
        }
        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await dBContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            //var existingWalk = await dBContext.Walks.FindAsync(id); //ignores include
            var existingWalk = await dBContext.Walks.FirstOrDefaultAsync(x=>x.Id==id);

            if (existingWalk == null) { return null; }
            existingWalk.Name = walk.Name;
            existingWalk.DifficultyId = walk.DifficultyId;
            existingWalk.RegionId = walk.RegionId;
            existingWalk.Description=walk.Description;
            existingWalk.WalkImageUrl=walk.WalkImageUrl;
            existingWalk.LengthInKms = walk.LengthInKms;

            await dBContext.SaveChangesAsync();
            return existingWalk;

        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var walk = await dBContext.Walks.FindAsync(id);
            if (walk == null) return null;
            dBContext.Walks.Remove(walk);
            await dBContext.SaveChangesAsync();
            return walk;

        }
    }
}
