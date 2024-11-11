namespace InMemoryRepositories;
using Entities;
using RepositoryContracts;

public class SubForumInMemoryRepository: ISubForumRepository
{
    private List<SubForum> subForums = new List<SubForum>();
    private readonly List<Post> posts = new();

    public SubForumInMemoryRepository()
    {
        _ = AddAsync(new SubForum("JAVA","Why is your favourite programming language not JAVA", 2)).Result;
        _ = AddAsync(new SubForum("JavaScript best","Dont you think that Javascript is much betteer, just the syntax is a littel tricky?", 3)).Result;
        _ = AddAsync(new SubForum("None of them","Many languages have their pros and cons in various ways, so you might have a favourite actually just depending on how much youre used to code with it", 4)).Result; //id = 1
        
        _ = AddAsync(new SubForum("Any horror movie recommendations","I have watched pretty much everything so please suggest something from some hidden gems", 1)).Result; //id = 2
        _ = AddAsync(new SubForum("Comedies","Same here with comedies!! We have wathed the whole Netflix so are there any suggestions for something good outside Netflix?", 3)).Result; //id = 2
        
        _ = AddAsync(new SubForum("Travel tips Italy","I want to make a small Milan trip for 4 days, what would you suggest to visit or which guide to take?", 4)).Result; //id = 3
        _ = AddAsync(new SubForum("Germany","Best bars or clubs in Berlin", 5)).Result; //id = 3
        _ = AddAsync(new SubForum("South America","I want to finally go out of Europe. What the best to visit in South America?", 2)).Result; //id = 3
        
        _ = AddAsync(new SubForum("Gym app","There are reasonable gym apps like Pro Gym workout", 2)).Result; //id = 4
        _ = AddAsync(new SubForum("How to follow a diet on an app","Is there and app to follow a diet", 4)).Result; //id = 4
        
        _ = AddAsync(new SubForum("5 day exercise routine","Its good to follow a 5 day exercise routine, like working out 3 days, 1 day rest, 2 day workout, 1 day rest", 5)).Result; //id = 5
        _ = AddAsync(new SubForum("Duration and time of the day" ,"Its good to have a short and rather intense workout ealry in the morning, even before having brekfast", 3)).Result; //id = 5
        
        _ = AddAsync(new SubForum("COD Warzone 2.0","How can you say no to some COD", 3)).Result; //id = 6
    }

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

    public Task<SubForum> AddASync(SubForum subForum)
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
    
    public IQueryable<SubForum> GetMany()
    {
        return subForums.AsQueryable();
    }

    public Task<SubForum> GetSingleAsync(int id)
    {
        return Task.FromResult(FindSubForumById(id));
    }
    public async Task<IEnumerable<Post>> GetPostsBySubforumAsync(int subforumId)
    {
        var subForum = subForums.FirstOrDefault(sf => sf.Id == subforumId);
        if (subForum == null)
        {
            throw new InvalidOperationException($"SubForum with ID '{subforumId}' not found.");
        }

        var subforumPosts = posts.Where(p => subForum.PostIds.Contains(p.Id));
        return await Task.FromResult(subforumPosts);
    }
    public async Task AddPostToSubforumAsync(int subforumId, int postId)
    {
        var subForum = FindSubForumById(subforumId);
        subForum.AddPost(postId);
        await Task.CompletedTask;
    }
}