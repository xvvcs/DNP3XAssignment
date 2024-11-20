namespace Entities;

public class User
{
    public int Id { get; set; } // primary key
    public string Username { get; set; }
    public string Password { get; set; }
    
    public List<Reaction> Reactions { get; set; } = []; // Collection navigation property for reactions
    public List<Post> Posts { get; set; } = []; // Collection navigation propert for posts
    public List<Comment> Comments { get; set; } = []; // Collection navigation property for comments
    public Moderator Moderator { get; set; } // Reference navigation property for moderator
    
    
    private User() { }
    public User(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public User(string username, string password, int id)
    {
        Id = id;
        Username = username;
        Password = password;
    }

   
}