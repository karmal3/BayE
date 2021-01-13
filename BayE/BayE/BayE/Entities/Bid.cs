using System;
using System.Collections.Generic;

namespace BayE.Entities
{
    public partial class Bid
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public int FkUserId { get; set; }
        public int FkAuctionAdId { get; set; }

        public virtual Auctionad FkAuctionAd { get; set; }
        public virtual User FkUser { get; set; }
    }
}
