using DTOs.SubForum;

namespace DTOs.Moderators;

public class ModeratorDTO
{
    public int UserId { get; set; }
    public int Id { get; set; }
   
    // Holds associated subforums
    public List<SubforumDTO> Subforums { get; set; } = new List<SubforumDTO>();
}