namespace DTOs.Moderators;

public class AddModeratorDTO
{
    public int Id { get; set; }
    public int UserId { get; set; }
    // List of Subforum IDs the moderator will manage
    public List<int> SubForumIds { get; set; } = new List<int>();
}