using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RepositoryContracts;

namespace EfcRepositories;

public class EfcUserRepository : IUserRepository
{
    private readonly AppContext _context;

    public EfcUserRepository(AppContext context)
    {
        _context = context;
    }
    
    public async Task UpdateAsync(User user)
    {
        if (!await _context.Users.AnyAsync(u => u.Id == user.Id))
        {
            throw new InvalidOperationException("User not found");
        }
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public Task<User> GetByUsernameAsync(string username)
    {
        return _context.Users.FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task DeleteAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            throw new InvalidOperationException("User not found");
        }
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User> FindByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }
    
    public async Task<User> AddASync(User user)
    {
        EntityEntry<User> entityEntry = await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task<User> GetSingleAsync(int id)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public IQueryable<User> GetMany()
    {
        return _context.Users.AsQueryable();
    }
}