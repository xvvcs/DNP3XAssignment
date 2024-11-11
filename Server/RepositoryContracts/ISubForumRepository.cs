using Entities;

namespace RepositoryContracts;

public interface ISubForumRepository
{
    Task<SubForum> AddASync(SubForum subForum);
    Task UpdateAsync(SubForum subForum);
    Task DeleteAsync(int id);
    Task<SubForum> GetSingleAsync(int id);
    IQueryable<SubForum> GetMany();
    public Task<int> FindSubForumCreator(int subForumID);
    Task<IEnumerable<Post>> GetPostsBySubforumAsync(int subforumId);
    Task AddPostToSubforumAsync(int subforumId, int postId);
    Task DeletePostFromSubforumAsync(int subforumId, int postId);
}