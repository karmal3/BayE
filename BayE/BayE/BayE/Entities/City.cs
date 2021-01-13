using System;
using System.Collections.Generic;

namespace BayE.Entities
{
    public partial class City
    {
        public City()
        {
            User = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}
