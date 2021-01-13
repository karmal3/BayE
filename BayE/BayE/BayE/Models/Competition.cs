﻿using BayE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BayE.Models
{
    public class Competition
    {
        public Competition()
        {
            Participant = new HashSet<Participant>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public DateTime Deadline { get; set; }
        public int FkAdminId { get; set; }
        public string Username { get; set; }

        public virtual Admin FkAdmin { get; set; }
        public virtual ICollection<Participant> Participant { get; set; }
    }
}
