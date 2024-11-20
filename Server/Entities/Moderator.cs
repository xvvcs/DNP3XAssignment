using System.Collections.Generic;

namespace Entities
{
    public class Moderator
    {
        public int UserId { get; set; }  // Foreign Key -> User
        public int SubForumId { get; set; } // Foreign Key -> SubForum
        
        // Primary key -> UserId + SubForumId (Composite Key)
        
        public User User { get; set; } // Reference navigation property for user
        public SubForum SubForum { get; set; } // Reference navigation property for subforum

        private Moderator() { }

        public Moderator(int UserId, int SubForumId)
        {
            this.UserId = UserId;
            this.SubForumId = SubForumId;
        }
    }
}