using System;
using System.Collections.Generic;

namespace BayE.Entities
{
    public partial class Topiccomments
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int FkUserId { get; set; }
        public int FkTopicId { get; set; }

        public virtual Topic FkTopic { get; set; }
        public virtual User FkUser { get; set; }
    }
}
