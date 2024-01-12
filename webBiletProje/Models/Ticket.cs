using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webBiletProje.Models
{
    public class Ticket
    {

        public int TicketId { get; set; }
        public string TName { get; set; }
        public string TImage { get; set; }
        public string Kind { get; set; }
        public string TDescription { get; set; }
        public string TDate { get; set; }
        public string TTime { get; set; }
        public string MainActor { get; set; }
        public int HallTicketAmount { get; set; }
        public int CurrentTicketAmount { get; set; }
        public int Price { get; set; }
        public string T_Address { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Rules { get; set; }
        public string Tmonth { get; set; }
    }
}