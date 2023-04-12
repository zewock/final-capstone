using System;

namespace Capstone.Models.DatabaseModles
{
    public class Forum
    {   // Model to return forum data
        public int forumId { get; set; }
        public string topic { get; set; }
        public int ownerId { get; set; }
        public DateTime createdDate { get; set; }
        public bool isVisible { get; set; } = true;

        public Forum()
        {

        }
        public Forum(int forumId, string topic, int ownerId, DateTime createdDate, bool isVisible)
        {
            this.forumId = forumId;
            this.topic = topic;
            this.ownerId = ownerId;
            this.createdDate = createdDate;
            this.isVisible = isVisible;
        }
    }
}
