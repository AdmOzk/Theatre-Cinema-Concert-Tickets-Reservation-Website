using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webBiletProje.Data;
using webBiletProje.Models;

namespace webBiletProje.Controllers
{
    public class AdminController : Controller
    {
        private webBiletProjeContext db = new webBiletProjeContext();

        // GET: Films
        [Authorize(Users ="gulsoyymuhammed@gmail.com")]

        public ActionResult Index()
        {
            return View(db.Tickets.ToList());
        }
        public ActionResult FilmSearch()
        {
            var cinemaTickets = GetFilms();
            return PartialView("_Films", cinemaTickets);
        }

        private List<Ticket> GetFilms()
        {
            return db.Tickets
                .Where(t => t.Kind == "film")
                .ToList();
        }

        public ActionResult TiyatroSearch()
        {
            var cinemaTickets = GetTiyatro();
            return PartialView("_Tiyatro", cinemaTickets);
        }

        private List<Ticket> GetTiyatro()
        {
            return db.Tickets
                .Where(t => t.Kind == "tiyatro")
                .ToList();
        }

        public ActionResult KonserSearch()
        {
            var cinemaTickets = GetKonser();
            return PartialView("_Konser", cinemaTickets);
        }

        private List<Ticket> GetKonser()
        {
            return db.Tickets
                .Where(t => t.Kind == "konser")
                .ToList();
        }
        // GET: Admin
    }
}