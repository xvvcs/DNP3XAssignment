using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class ManageUsersView
{
    private readonly IUserRepository userRepository;
    
    public ManageUsersView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task DelateUserAsync(int userID)
    {
        await userRepository.DeleteAsync(userID);
        Console.WriteLine($"User with ID {userID} deleted successfully.");
    }

    public async Task UpdateUserAsync(string username, string password, int userID)
    {
        User user = new User(username, password, userID);
        await userRepository.UpdateAsync(user);
        Console.WriteLine($"User with ID {userID} updated successfully.");
    }
}