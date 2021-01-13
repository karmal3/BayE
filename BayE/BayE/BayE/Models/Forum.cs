using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BayE.Models
{
    public class Forum
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ViewCount { get; set; }
        public DateTime Date { get; set; }
        public int FkUserId { get; set; }
        public string Username { get; set; }
        public int Total { get; set; }
    }
}
