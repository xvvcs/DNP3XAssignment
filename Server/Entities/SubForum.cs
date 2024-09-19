namespace Entities;

public class SubForum
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int UserId { get; set; }
    public int PostId { get; set; }

    
    public SubForum() { }
    public SubForum(string title, string description, int userId)
    {
        Title = title;
        Description = description;
        UserId = userId;
        PostId = 0; // Initial post ID, will be set when the first post is created in the forum.
    }

}