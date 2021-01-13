using System;
using System.Collections.Generic;

namespace BayE.Entities
{
    public partial class User
    {
        public User()
        {
            Ad = new HashSet<Ad>();
            Adcomments = new HashSet<Adcomments>();
            Admin = new HashSet<Admin>();
            Auctionad = new HashSet<Auctionad>();
            Autobid = new HashSet<Autobid>();
            Bid = new HashSet<Bid>();
            Blockeduser = new HashSet<Blockeduser>();
            Forum = new HashSet<Forum>();
            Participant = new HashSet<Participant>();
            Payment = new HashSet<Payment>();
            PrivatemessageFkReceiver = new HashSet<Privatemessage>();
            PrivatemessageFkSender = new HashSet<Privatemessage>();
            Topic = new HashSet<Topic>();
            Topiccomments = new HashSet<Topiccomments>();
            Viewedad = new HashSet<Viewedad>();
            Wishlist = new HashSet<Wishlist>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int FkCityId { get; set; }
        public byte[] Hash { get; set; }
        public byte[] Salt { get; set; }

        public virtual City FkCity { get; set; }
        public virtual ICollection<Ad> Ad { get; set; }
        public virtual ICollection<Adcomments> Adcomments { get; set; }
        public virtual ICollection<Admin> Admin { get; set; }
        public virtual ICollection<Auctionad> Auctionad { get; set; }
        public virtual ICollection<Autobid> Autobid { get; set; }
        public virtual ICollection<Bid> Bid { get; set; }
        public virtual ICollection<Blockeduser> Blockeduser { get; set; }
        public virtual ICollection<Forum> Forum { get; set; }
        public virtual ICollection<Participant> Participant { get; set; }
        public virtual ICollection<Payment> Payment { get; set; }
        public virtual ICollection<Privatemessage> PrivatemessageFkReceiver { get; set; }
        public virtual ICollection<Privatemessage> PrivatemessageFkSender { get; set; }
        public virtual ICollection<Topic> Topic { get; set; }
        public virtual ICollection<Topiccomments> Topiccomments { get; set; }
        public virtual ICollection<Viewedad> Viewedad { get; set; }
        public virtual ICollection<Wishlist> Wishlist { get; set; }
    }
}
