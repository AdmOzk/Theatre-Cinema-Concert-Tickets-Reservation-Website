using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace webBiletProje.Models
{
    public class Orders
    {
        [Key] // Specify the primary key
        public int OrderID { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Category { get; set; }
        public string City { get; set; }
        public string Salon { get; set; }

        public string TicketDate { get; set; }
        public string TicketHour { get; set; }
        public string TicketName { get; set; }

        public string Seat { get; set; }
    }
}