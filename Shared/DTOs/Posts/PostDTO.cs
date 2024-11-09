namespace DTOs.Posts;

public class PostDTO
{
    public string Title { get; set; }
    public string Body { get; set; }
    public int UserId { get; set; }
    public int Id { get; set; }
    public List<CommentDTO> Comments { get; set; }
    public bool ShowComments { get; set; }
    public int LikeCount { get; set; }
    public int DislikeCount { get; set; }
}