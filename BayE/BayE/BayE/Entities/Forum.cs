using System;
using System.Collections.Generic;

namespace BayE.Entities
{
    public partial class Forum
    {
        public Forum()
        {
            Topic = new HashSet<Topic>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ViewCount { get; set; }
        public DateTime Date { get; set; }
        public int FkUserId { get; set; }

        public virtual User FkUser { get; set; }
        public virtual ICollection<Topic> Topic { get; set; }
    }
}
