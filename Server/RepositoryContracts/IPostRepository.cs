using Entities;

namespace RepositoryContracts;

public interface IPostRepository
{
    Post FindPost(Post post);
    Post FindPostById(int id);
    Task<Post> AddASync (Post post);
    Task<Post> GetSingleAsync (int id);
    Task DeleteAsync (int id);
    Task UpdateAsync (Post post);
    IQueryable<Post> GetMany();
    Task LikeAsync (Post post);
    Task DisLikeAsync (Post post);
}