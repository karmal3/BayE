using System;
using System.Collections.Generic;

namespace BayE.Entities
{
    public partial class Topic
    {
        public Topic()
        {
            Topiccomments = new HashSet<Topiccomments>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int ViewCount { get; set; }
        public int FkUserId { get; set; }
        public int FkForumId { get; set; }

        public virtual Forum FkForum { get; set; }
        public virtual User FkUser { get; set; }
        public virtual ICollection<Topiccomments> Topiccomments { get; set; }
    }
}
