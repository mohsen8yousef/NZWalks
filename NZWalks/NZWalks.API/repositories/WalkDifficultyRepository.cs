using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.repositories
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public WalkDifficultyRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<WalkDefficulty> AddAsync(WalkDefficulty walkDefficulty)
        {
            walkDefficulty.Id = Guid.NewGuid();
            await nZWalksDbContext.WalkDefficulties.AddAsync(walkDefficulty);
            await nZWalksDbContext.SaveChangesAsync();
            return walkDefficulty;
        }

       
        public async Task<WalkDefficulty> DeleteAsync(Guid id)
        {
            var existingWalkDifficulty = await nZWalksDbContext.WalkDefficulties.FindAsync(id);

            if (existingWalkDifficulty == null)
            {
                return null;
            }

            nZWalksDbContext.WalkDefficulties.Remove(existingWalkDifficulty);
            await nZWalksDbContext.SaveChangesAsync();
            return existingWalkDifficulty;
        }

        public async Task<IEnumerable<WalkDefficulty>> GetAllAsync()
        {
            return await nZWalksDbContext.WalkDefficulties.ToListAsync();
        }

        public async Task<WalkDefficulty> GetAsync(Guid id)
        {
            return await nZWalksDbContext.WalkDefficulties.FirstOrDefaultAsync(x => x.Id == id);
        }

        
        public async Task<WalkDefficulty> UpdateAsync(Guid id, WalkDefficulty walkDefficulty)
        {
            var existingWalkDifficulty = await nZWalksDbContext.WalkDefficulties.FindAsync(id);

            if (existingWalkDifficulty == null)
            {
                return null;
            }

            existingWalkDifficulty.Code = walkDefficulty.Code;
            await nZWalksDbContext.SaveChangesAsync();
            return existingWalkDifficulty;
        }
    }
}
