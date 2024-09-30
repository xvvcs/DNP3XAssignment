namespace Entities;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public int UserId { get; set; }
    public List<int> Like { get; set; } = new List<int>();
    public int LikeCount { get; set; }
    public List<int> Dislike { get; set; } = new List<int>();
    public int DislikeCount { get; set; }

    public Post() { }
    public Post(string Title, string Body, int UserId)
    {
        this.Body = Body;
        this.Title = Title;
        this.UserId = UserId;
        LikeCount = Like.Count;
        DislikeCount = Dislike.Count;
    }

    public Post(string Title, string Body, int userID, int Id)
    {
        this.Id = Id;
        this.Title = Title;
        this.Body = Body;
        this.UserId = userID;
        LikeCount = Like.Count;
        DislikeCount = Dislike.Count;
    }
    
    public void updateLikeCount()
    {
        LikeCount = Like.Count;
        DislikeCount = Dislike.Count;
    }
}