namespace DTOs.Posts;

public class CommentDTO
{
    public string Body { get; set; }
    public int PostId { get; set; }
    public int UserId { get; set; }
    public int Id { get; set; }
}