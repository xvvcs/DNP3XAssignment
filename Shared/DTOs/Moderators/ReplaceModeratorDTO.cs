namespace DTOs.Moderators;

public class ReplaceModeratorDTO
{
    public int UserId { get; set; }
    
    public List<int> SubForumIds { get; set; } = new List<int>();
}