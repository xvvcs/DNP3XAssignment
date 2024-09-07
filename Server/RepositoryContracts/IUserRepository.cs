using Entities;

namespace RepositoryContracts;

public interface IUserRepository
{
    Task<User> AddASync (User user);
    Task<User> GetSingleAsync (int id);
    Task DeleteAsync (int id);
    Task UpdateAsync (User user);
    IQueryable<User> GetMany();
}