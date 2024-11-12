using System.Collections.Generic;

namespace Entities
{
    public class Moderator
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        // Collection of subforum IDs that this moderator is associated with
        public List<int> SubForumIds { get; set; } = new List<int>();

        public Moderator() { }

        public Moderator(int userId, List<int> subForumIds)
        {
            this.UserId = userId;
            this.SubForumIds = subForumIds;
        }

        public Moderator(int moderatorId, int userId, List<int> subForumIds)
        {
            this.Id = moderatorId;
            this.UserId = userId;
            this.SubForumIds = subForumIds;
        }
    }
}