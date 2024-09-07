using Entities;

namespace RepositoryContracts;

public interface IPostRepository
{
    Task<Post> AddASync (Post post);
    Task<Post> GetSingleAsync (int id);
    Task DeleteAsync (int id);
    Task UpdateAsync (Post post);
    IQueryable<Post> GetMany();
}