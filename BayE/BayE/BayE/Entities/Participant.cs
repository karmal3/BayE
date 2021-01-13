using System;
using System.Collections.Generic;

namespace BayE.Entities
{
    public partial class Participant
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int FkUserId { get; set; }
        public int FkCompetitionId { get; set; }

        public virtual Competition FkCompetition { get; set; }
        public virtual User FkUser { get; set; }
    }
}
