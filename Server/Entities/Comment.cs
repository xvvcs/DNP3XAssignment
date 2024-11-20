namespace Entities;

public class Comment
{
    public int Id { get; set; } // primary key
    public string Body { get; set; }
    public int PostId { get; set; } // foreign key - > Post
    public int UserId { get; set; } // foreign key - > User
    
    public Post Post { get; set; } // Reference navigation property for post
    public User User { get; set; } // Reference navigation property for user
    public List<Reaction> Reactions { get; set; } = []; // Collection navigation property for reactions
    
    private Comment() { }
    public Comment(string body, int postID, int userID)
    {
        Body = body;
        PostId = postID;
        UserId = userID;
    }
    public Comment(int id, string body, int postID, int userID)
    {
        Id = id;
        Body = body;
        PostId = postID;
        UserId = userID;
    }
}