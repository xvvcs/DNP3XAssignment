using Entities;

namespace RepositoryContracts;

public interface IPostRepository
{
    Task<Post> FindPostByIdAsync(int id);
    Task<Post> AddASync (Post post);
    Task<Post> GetSingleAsync (int id);
    Task DeleteAsync (int id);
    Task UpdateAsync (Post post);
    IQueryable<Post> GetMany();
    Task LikeAsync (Post post, int userID);
    Task DisLikeAsync (Post post, int userID);
}