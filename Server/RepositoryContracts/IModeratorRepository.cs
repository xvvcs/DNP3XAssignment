using Entities;

namespace RepositoryContracts;

public interface IModeratorRepository
{
    Task<Moderator> GetSingleAsync(int id);
    Task UpdateAsync(Moderator moderator);
    Task DeleteAsync(int id);
    Task<Moderator> AddAsync(Moderator moderator);
    IQueryable<Moderator> GetManyAsync();
    Task<List<Moderator>> GetModeratorsBySubForumIdAsync(int subForumId);
}