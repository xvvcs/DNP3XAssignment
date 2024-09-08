namespace Entities;

public class Comment
{
    public int Id { get; set; }
    public string Body { get; set; }
    public int PostId { get; set; }
    public int UserId { get; set; }
    public int LikeCount { get; set; }
    public int DislikeCount { get; set; }
}