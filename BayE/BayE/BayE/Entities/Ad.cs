using System;
using System.Collections.Generic;

namespace BayE.Entities
{
    public partial class Ad
    {
        public Ad()
        {
            Adcomments = new HashSet<Adcomments>();
            Payment = new HashSet<Payment>();
            Viewedad = new HashSet<Viewedad>();
            Wishlist = new HashSet<Wishlist>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public int Status { get; set; }
        public int FkUserId { get; set; }
        public int FkCategoryId { get; set; }
        public int FkSubcategoryId { get; set; }

        public virtual Adcategory FkCategory { get; set; }
        public virtual Adsubcategory FkSubcategory { get; set; }
        public virtual User FkUser { get; set; }
        public virtual Status StatusNavigation { get; set; }
        public virtual ICollection<Adcomments> Adcomments { get; set; }
        public virtual ICollection<Payment> Payment { get; set; }
        public virtual ICollection<Viewedad> Viewedad { get; set; }
        public virtual ICollection<Wishlist> Wishlist { get; set; }
    }
}
