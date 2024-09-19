namespace Entities;

public class Moderator
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int SubForumId { get; set; }

    public Moderator() { }
    public Moderator(int userId, int subForumId)
    {
       this.UserId = userId;
       this.SubForumId = subForumId;
       
       // ID is handled in ModeratorInMemoryRepository in case of creation
    }

    public Moderator(int moderatorId, int userId, int subForumId)
    {
        this.Id = moderatorId;
        this.UserId = userId;
        this.SubForumId = subForumId;
    }
}