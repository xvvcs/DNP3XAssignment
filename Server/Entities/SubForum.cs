namespace Entities;

public class SubForum
{
    public int Id { get; set; }  // primary key
    public string Title { get; set; }
    public string? Description { get; set; }
    public List<Post> Posts { get; set; } = []; // Collection navigation property for posts
    public List<Moderator> Moderators { get; set; } = []; // Collection navigation property for moderators
    
    private SubForum() { }
    public SubForum(string title, string description)
    {
        Title = title;
        Description = description;
        
    }
    public SubForum(string title, string description, int id)
    {
        Id = id;
        Title = title;
        Description = description;
    }
}