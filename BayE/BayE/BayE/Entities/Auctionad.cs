using System;
using System.Collections.Generic;

namespace BayE.Entities
{
    public partial class Auctionad
    {
        public Auctionad()
        {
            Autobid = new HashSet<Autobid>();
            Bid = new HashSet<Bid>();
            Payment = new HashSet<Payment>();
            Viewedad = new HashSet<Viewedad>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public DateTime Length { get; set; }
        public int Status { get; set; }
        public int FkUserId { get; set; }
        public int FkCategoryId { get; set; }
        public int FkSubcategoryId { get; set; }

        public virtual Adcategory FkCategory { get; set; }
        public virtual Adsubcategory FkSubcategory { get; set; }
        public virtual User FkUser { get; set; }
        public virtual Status StatusNavigation { get; set; }
        public virtual ICollection<Autobid> Autobid { get; set; }
        public virtual ICollection<Bid> Bid { get; set; }
        public virtual ICollection<Payment> Payment { get; set; }
        public virtual ICollection<Viewedad> Viewedad { get; set; }
    }
}
