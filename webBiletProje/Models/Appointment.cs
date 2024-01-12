using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace webBiletProje.Models
{
    public class Appointment
    {
        public DateTime Tarih { get; set; }
        public TimeSpan Saatler { get; set; }
        public int Kalan { get; set; }
        public string Bölümler { get; set; }
    }

    public class sehir
    {

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public string Inactive { get; set; }
        public string Adres { get; set; }
        public string Link { get; set; }

        public int SehirId { get; set; }

    }

    public class etkinlikdetay
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int EtkinlikId { get; set; }
    }

    public class Etkinlik
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class salon
    {
        [Key]
        public int Id { get; set; }


        public string Depart { get; set; }

        public int Total { get; set; }

        public int SubeId { get; set; }

        public string Inactive { get; set; }
        // Other properties for Department
    }
}
