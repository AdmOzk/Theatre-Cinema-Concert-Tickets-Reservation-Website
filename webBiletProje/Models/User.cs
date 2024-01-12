using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace webBiletProje.Models
{
    public class User
    {
        [Key] // Specify the primary key
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Sex { get; set; }
        public string MailAddress { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}