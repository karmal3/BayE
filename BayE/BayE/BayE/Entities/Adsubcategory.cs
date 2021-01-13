using System;
using System.Collections.Generic;

namespace BayE.Entities
{
    public partial class Adsubcategory
    {
        public Adsubcategory()
        {
            Ad = new HashSet<Ad>();
            Auctionad = new HashSet<Auctionad>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int FkCategoryId { get; set; }

        public virtual Adcategory FkCategory { get; set; }
        public virtual ICollection<Ad> Ad { get; set; }
        public virtual ICollection<Auctionad> Auctionad { get; set; }
    }
}
