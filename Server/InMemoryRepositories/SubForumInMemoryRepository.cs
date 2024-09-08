namespace InMemoryRepositories;
using Entities;
using RepositoryContracts;

public class SubForumInMemoryRepository
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
}