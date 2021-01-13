using System;
using System.Collections.Generic;

namespace BayE.Entities
{
    public partial class Payment
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public string CardHolder { get; set; }
        public string CardNumber { get; set; }
        public int? FkUserId { get; set; }
        public int? FkAdId { get; set; }
        public int? FkAuctionAdId { get; set; }

        public virtual Ad FkAd { get; set; }
        public virtual Auctionad FkAuctionAd { get; set; }
        public virtual User FkUser { get; set; }
    }
}
