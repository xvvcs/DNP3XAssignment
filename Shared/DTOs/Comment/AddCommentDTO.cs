namespace DTOs.Posts;

public class AddCommentDTO
{
    public string Body { get; set; }
    public int PostId { get; set; }
    public int UserId { get; set; }
}