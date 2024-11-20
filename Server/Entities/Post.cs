namespace Entities;

public class Post
{
    public int Id { get; set; }  // primary key
    public string Title { get; set; }
    public string Body { get; set; }
    public int UserId { get; set; } // foreign key - > User
    public int? SubForumId { get; set; }  // foreign key -> SubForum  
    
    public User User { get; set; } // Reference navigation property for user
    public SubForum SubForum { get; set; } // Reference navigation property for subforum
    public List<Comment> Comments { get; set; } = []; // Collection navigation property for comments
    public List<Reaction> Reactions { get; set; } = []; // Collection navigation property for reactions
    

    private Post() { }
    public Post(string Title, string Body, int UserId)
    {
        this.Body = Body;
        this.Title = Title;
        this.UserId = UserId;
    }

    public Post(string Title, string Body, int UserId, int Id, int SubForumId )
    {
        this.Id = Id;
        this.Title = Title;
        this.Body = Body;
        this.UserId = UserId;
        this.SubForumId = SubForumId;
    }
}