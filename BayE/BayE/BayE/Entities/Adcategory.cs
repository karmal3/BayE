using System;
using System.Collections.Generic;

namespace BayE.Entities
{
    public partial class Adcategory
    {
        public Adcategory()
        {
            Ad = new HashSet<Ad>();
            Adsubcategory = new HashSet<Adsubcategory>();
            Auctionad = new HashSet<Auctionad>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Ad> Ad { get; set; }
        public virtual ICollection<Adsubcategory> Adsubcategory { get; set; }
        public virtual ICollection<Auctionad> Auctionad { get; set; }
    }
}
