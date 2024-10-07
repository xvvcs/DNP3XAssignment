namespace Entities;

public class Comment
{
    public int Id { get; set; }
    public string Body { get; set; }
    public int PostId { get; set; }
    public int UserId { get; set; }
    public int LikeCount { get; set; }
    public List<int> Like { get; set; } = new List<int>();
    public int DislikeCount { get; set; }
    public List<int> Dislike { get; set; } = new List<int>();

    public Comment() { }
    public Comment(string body, int postID, int userID)
    {
        Body = body;
        PostId = postID;
        UserId = userID;
        LikeCount = Like.Count;
        DislikeCount = Dislike.Count;
    }

    public Comment(string body, int postID, int userID, int id)
    {
        Id = id;
        Body = body;
        PostId = postID;
        UserId = userID;
        LikeCount = Like.Count;
        DislikeCount = Dislike.Count;
    }
    
    public void updateLikeCount()
    {
        LikeCount = Like.Count;
        DislikeCount = Dislike.Count;
    }
}