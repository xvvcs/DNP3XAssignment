namespace Entities;

public class Moderator
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int SubForumId { get; set; }

    public Moderator(int userId, int subForumId)
    {
       this.UserId = userId;
       this.SubForumId = subForumId;
       
       // ID is handled in ModeratorInMemoryRepository
    }
    
}