using DTOs.SubForum;

namespace DTOs.Moderators;

public class ModeratorDTO
{
    public int UserId { get; set; }
    public int Id { get; set; }
    
    public List<int> Subforums { get; set; } = new List<int>();
}