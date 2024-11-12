namespace DTOs.Moderators;

public class AddModeratorDTO
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public List<int> SubForumIds { get; set; } = new List<int>();
}