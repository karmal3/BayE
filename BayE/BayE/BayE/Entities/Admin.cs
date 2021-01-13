using System;
using System.Collections.Generic;

namespace BayE.Entities
{
    public partial class Admin
    {
        public Admin()
        {
            Blockeduser = new HashSet<Blockeduser>();
            Competition = new HashSet<Competition>();
        }

        public int Id { get; set; }
        public int FkUserId { get; set; }
        public DateTime Date { get; set; }

        public virtual User FkUser { get; set; }
        public virtual ICollection<Blockeduser> Blockeduser { get; set; }
        public virtual ICollection<Competition> Competition { get; set; }
    }
}
