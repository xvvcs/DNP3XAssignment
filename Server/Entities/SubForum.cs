namespace Entities;

public class SubForum
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int UserId { get; set; }
    public int PostId { get; set; }
}