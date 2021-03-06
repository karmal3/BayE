﻿using System;
using System.Collections.Generic;

namespace BayE.Entities
{
    public partial class Viewedad
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public int FkUserId { get; set; }
        public int? FkAdId { get; set; }
        public int? FkAuctionAdId { get; set; }

        public virtual Ad FkAd { get; set; }
        public virtual Auctionad FkAuctionAd { get; set; }
        public virtual User FkUser { get; set; }
    }
}
