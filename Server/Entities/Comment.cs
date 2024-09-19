namespace Entities;

public class Comment
{
    public int Id { get; set; }
    public string Body { get; set; }
    public int PostId { get; set; }
    public int UserId { get; set; }
    public int LikeCount { get; set; }
    public int DislikeCount { get; set; }

    public Comment() { }
    public Comment(string body, int postID, int userID)
    {
        Body = body;
        PostId = postID;
        UserId = userID;
        LikeCount = 0;
        DislikeCount = 0;
    }

    public Comment(string body, int postID, int userID, int likeCount, int dislikeCount, int id)
    {
        Id = id;
        Body = body;
        PostId = postID;
        UserId = userID;
        LikeCount = likeCount;
        DislikeCount = dislikeCount;
    }
}