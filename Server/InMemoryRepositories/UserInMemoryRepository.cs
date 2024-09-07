namespace InMemoryRepositories;
using Entities;
using RepositoryContracts;

public class UserInMemoryRepository: IUserRepository
    
{
    List<User> users = new List<User>();
    
    public User FindUser(User user)
    {
        User? exisitingUser = users.FirstOrDefault(p => p.Id == user.Id);
        if (exisitingUser is null)
        {
            throw new InvalidOperationException($"User with ID '{user.Id}'not found.");
        }
        return exisitingUser;
    }
    public User FindUserById(int id)
    {
        User? exisitingUser = users.FirstOrDefault(p => p.Id == id);
        if (exisitingUser is null)
        {
            throw new InvalidOperationException($"User with ID '{id}'not found.");
        }
        return exisitingUser;
    }

    public Task<User> AddASync(User user)
    {
        user.Id = users.Any()
            ? users.Max(p => p.Id) + 1
             : 1;
        users.Add(user);
        return Task.FromResult(user);
    }

    public Task<User> GetSingleAsync(int id)
    {
        return Task.FromResult(FindUserById(id));
    }

    public Task DeleteAsync(int id)
    {
        users.Remove(FindUserById(id));
        return Task.CompletedTask;
    }

    public Task UpdateAsync(User user)
    {
        users.Remove(FindUser(user));
        users.Add(user);
        return Task.CompletedTask;
    }

    public IQueryable<User> GetMany()
    {
        return users.AsQueryable();
    }
}