using System;
using System.Collections.Generic;

namespace BayE.Entities
{
    public partial class Blockeduser
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Reason { get; set; }
        public int FkAdminId { get; set; }
        public int FkUserId { get; set; }

        public virtual Admin FkAdmin { get; set; }
        public virtual User FkUser { get; set; }
    }
}
