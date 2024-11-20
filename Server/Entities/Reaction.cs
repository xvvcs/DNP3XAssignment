namespace Entities;

public class Reaction
{
    public int ReactionId { get; set; }  // primary key
    public int UserId { get; set; } // foreign key - > User
    
    public int? PostId { get; set; } // foreign key - > Post
    
    public int? CommentId { get; set; } // foreign key - > Comment
    public bool isPost { get; set; } // true if it is a post, false if it is a comment
    public bool IsLike { get; set; } // true if it is a like, false if it is a dislike
    
    public User User { get; set; } // Reference navigation property for user
    public Comment Comment { get; set; } // Reference navigation property for comment
    public Post Post { get; set; } // Reference navigation property for post
    
    
    
    //TODO ensure that when adding new reaction to set the isPost property correctly. We will handle it in the business logic since it is too complex to handle it on database level
    //TODO both the commentId and postId can be null, but one of them must be inputted. We need to ensure it in business logic
    
    private Reaction() { }
    
    public Reaction(int userId, int postId, bool isLike)  // constructor for post
    {
        UserId = userId;
        PostId = postId;
        IsLike = isLike;
        isPost = true;
    }

    public Reaction(int userId, int commentId, bool isLike, bool isPost = false)  // constructor for comment
    {
        UserId = userId;
        CommentId = commentId;
        IsLike = isLike;
        this.isPost = isPost;
    }
}