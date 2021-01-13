using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BayE.Models
{
    public class Ad
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public int FkUserId { get; set; }
        public int FkCategoryId { get; set; }
        public int FkSubcategoryId { get; set; }
        public string Username { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
    }
}
