namespace DTOs.Moderators;

public class ReplaceModeratorDTO
{
    public int UserId { get; set; }
    // List of Subforum IDs the moderator will manage
    public List<int> SubForumIds { get; set; } = new List<int>();
}