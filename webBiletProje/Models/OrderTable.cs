using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace webBiletProje.Models
{
    public class OrderTable
    {
        [Key] // Specify the primary key
        public int OrderID { get; set; }
        public int UserId { get; set; }
        public string OrderDate { get; set; }
        public int TicketId { get; set; }
    }
}