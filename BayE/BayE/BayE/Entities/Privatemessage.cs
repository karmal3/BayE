using System;
using System.Collections.Generic;

namespace BayE.Entities
{
    public partial class Privatemessage
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int FkSenderId { get; set; }
        public int FkReceiverId { get; set; }
        public byte ReceiverDeleted { get; set; }
        public byte SenderDeleted { get; set; }
        public byte ReceiverRead { get; set; }

        public virtual User FkReceiver { get; set; }
        public virtual User FkSender { get; set; }
    }
}
