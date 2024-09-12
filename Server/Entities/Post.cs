namespace Entities;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public int UserId { get; set; }
    public int LikeCount { get; set; }
    public int DislikeCount { get; set; }

    public Post(string Title, string Body, int UserId)
    {
        this.Body = Body;
        this.Title = Title;
        this.UserId = UserId;
        LikeCount = 0;
        DislikeCount = 0;
    }

    public Post(string Title, string Body, int userID, int likeCount, int dislikeCount, int Id)
    {
        this.Id = Id;
        this.Title = Title;
        this.Body = Body;
        this.UserId = userID;
        this.LikeCount = likeCount;
        this.DislikeCount = dislikeCount;
    }
}