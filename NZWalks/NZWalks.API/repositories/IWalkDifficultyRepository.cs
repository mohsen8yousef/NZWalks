using NZWalks.API.Models.Domain;

namespace NZWalks.API.repositories
{
    public interface IWalkDifficultyRepository
    {
        Task<IEnumerable<WalkDefficulty>> GetAllAsync();

        Task<WalkDefficulty> GetAsync(Guid id);

        Task<WalkDefficulty> AddAsync(WalkDefficulty walkDefficulty);

        Task<WalkDefficulty> UpdateAsync(Guid id, WalkDefficulty walkDefficulty);
        Task<WalkDefficulty> DeleteAsync (Guid id);
    }
}
