using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.repositories
{
    public class WalksRepository : IWalksRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public WalksRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }
        public async Task<IEnumerable<Walk>> GetAllAsync()
        {
            return await nZWalksDbContext.Walks
                .Include(x => x.Region)
                .Include(x => x.WalkDefficulty)
                .ToListAsync();
        }

        public  Task<Walk> GetAsync(Guid id)
        {
            return  nZWalksDbContext.Walks
                .Include(x => x.Region)
                .Include(x => x.WalkDefficulty)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async  Task<Walk> AddWalkAsync(Walk walk)
        {
            walk.Id = Guid.NewGuid();
            await nZWalksDbContext.Walks.AddAsync(walk);
            await nZWalksDbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk> UpdateWalkAsync(Guid id, Walk walk)
        {
            var existingWalk = await nZWalksDbContext.Walks.FindAsync(id);
            if (existingWalk != null)
            {
                existingWalk.length = walk.length;
                existingWalk.Name = walk.Name;
                existingWalk.WalkDefficultyId = walk.WalkDefficultyId;
                await nZWalksDbContext.SaveChangesAsync();
                return existingWalk;
            }
            return null;
            
        }

        public async Task<Walk> DeleteWalkAsync(Guid id)
        {
            var existingWalk = await nZWalksDbContext.Walks.FindAsync(id);
            if (existingWalk != null)
            {
                nZWalksDbContext.Walks.Remove(existingWalk);
                await nZWalksDbContext.SaveChangesAsync();
                return existingWalk;
            }

            return null;
                
        }
    }
}
