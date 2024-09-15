namespace InMemoryRepositories;
using Entities;
using RepositoryContracts;

public class SubForumInMemoryRepository: ISubForumRepository
{
    private List<SubForum> subForums = new List<SubForum>();

    public SubForum FindSubForum(SubForum subForum)
    {
        SubForum? exisitingSubForum = subForums.FirstOrDefault(s => s.Id == subForum.Id);
        if (exisitingSubForum is null)
        {
            throw new InvalidOperationException($"SubForum with ID '{subForum.Id}' not found.");
        }

        return exisitingSubForum;
    }

    public SubForum FindSubForumById(int id)
    {
        SubForum? exisitingSubForum = subForums.FirstOrDefault(s => s.Id == id);
        if (exisitingSubForum is null)
        {
            throw new InvalidOperationException($"SubForum with ID '{id}' not found.");
        }

        return exisitingSubForum;
    }

    public Task<SubForum> AddAsync(SubForum subForum)
    {
        subForum.Id = subForums.Any()
           ? subForums.Max(s => s.Id) + 1
            : 1;
        subForums.Add(subForum);
        return Task.FromResult(subForum);
    }

    public Task UpdateAsync(SubForum subForum)
    {
      subForums.Remove(FindSubForum(subForum));
      subForums.Add(subForum);
      
      return Task.CompletedTask;
    }

    public Task<int> FindSubForumCreator(int subForumID)
    {
        return Task.FromResult(FindSubForumById(subForumID).UserId);
    }

    public Task DeleteAsync(int id)
    {
        subForums.Remove(FindSubForumById(id));
        return Task.CompletedTask;
    }
    
    public IQueryable<SubForum> GetManyAsync()
    {
        return subForums.AsQueryable();
    }

    public Task<SubForum> GetSingleAsync(int id)
    {
        return Task.FromResult(FindSubForumById(id));
    }
}