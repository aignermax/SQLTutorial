using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public WalkDifficultyRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }
        public async Task<WalkDifficulty> AddWalkDifficultyAsync(WalkDifficulty walkDifficulty)
        {
            walkDifficulty.Id = Guid.NewGuid();
            await nZWalksDbContext.AddAsync(walkDifficulty);
            await nZWalksDbContext.SaveChangesAsync();
            return walkDifficulty;
        }

        public async Task<WalkDifficulty> DeleteWalkDifficultyAsync(Guid id)
        {
            var walkDifficultyToDelete = await nZWalksDbContext.WalkDifficulty.FindAsync(id);
            if(walkDifficultyToDelete == null) { return null; }
            nZWalksDbContext.WalkDifficulty.Remove(walkDifficultyToDelete);
            await nZWalksDbContext.SaveChangesAsync();
            return walkDifficultyToDelete;

        }

        public async Task<WalkDifficulty> GetAsync(Guid id)
        {
            return await nZWalksDbContext.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id );
        }

        public async Task<IEnumerable<WalkDifficulty>> GetAllAsync()
        {
            return await nZWalksDbContext.WalkDifficulty.ToListAsync();
        }

        public async Task<WalkDifficulty> UpdateWalkDifficultyAsync(Guid id , WalkDifficulty walkDifficulty)
        {
            var walkDifficultyDatabase = await GetAsync(id);
            if (walkDifficultyDatabase == null) return null;
            walkDifficultyDatabase.Code = walkDifficulty.Code;
            await nZWalksDbContext.SaveChangesAsync();
            return walkDifficultyDatabase;
        }
    }
}
