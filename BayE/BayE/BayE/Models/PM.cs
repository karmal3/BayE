using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BayE.Models
{
    public class PM
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int FkSenderId { get; set; }
        public int FkReceiverId { get; set; }
        public string SenderUsername { get; set; }
        public string ReceiverUsername { get; set; }
    }
}
