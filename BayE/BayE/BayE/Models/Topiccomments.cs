using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BayE.Models
{
    public class Topiccomments
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int FkUserId { get; set; }
        public int FkTopicId { get; set; }
        public string Username { get; set; }
    }
}
