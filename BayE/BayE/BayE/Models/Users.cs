using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BayE.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int FkCityId { get; set; }
        public bool Role { get; set; }
        public bool Blocked { get; set; }
    }
}
