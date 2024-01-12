using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using webBiletProje.Data;
using webBiletProje.Models;



namespace webBiletProje.Controllers
{
    public class HomeController : Controller
    {

        private webBiletProjeContext db = new webBiletProjeContext();
        public ActionResult film()
        {
            return View(db.Tickets.ToList());
        }



        public ActionResult filmInfo(int? id)
        {

            Ticket ticket =db.Tickets.Find(id);
            return View(ticket);
        }


        public ActionResult tiyatro()
        {
            return View(db.Tickets.ToList());
        }




        public ActionResult tiyatroInfo(int? id)
        {

            Ticket ticket = db.Tickets.Find(id);
            return View(ticket);
        }






        public ActionResult konser()
        {
            return View(db.Tickets.ToList());
        }


        public ActionResult konserInfo(int? id)
        {

            Ticket ticket = db.Tickets.Find(id);
            return View(ticket);
        }



        private readonly DataContext _context;

        public HomeController()
        {
            _context = new DataContext(); // Replace YourDbContext with the actual name of your DbContext class
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(string userName, string password)
        {
            var user = await _context.Set<User>().SingleOrDefaultAsync(u => u.UserName == userName && u.Password == password);

            if (user != null)
            {
                // Login successful, redirect to Index view
                return RedirectToAction("Index");
            }
            else
            {
                // Login failed, you might want to show an error message or redirect back to the login page
                ViewBag.ErrorMessage = "Invalid username or password.";
                return View();
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User userModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Assuming you have a DbContext class named DataContext
                    using (var dbContext = new DataContext())
                    {
                        // Map User model to your entity model (assuming your entity model is named UserEntity)
                        var userEntity = new User
                        {
                            Name = userModel.Name,
                            Surname = userModel.Surname,
                            MailAddress = userModel.MailAddress,
                            PhoneNumber = userModel.PhoneNumber,
                            Sex = userModel.Sex
                        };

                        // Add the entity to the database
                        dbContext.Users.Add(userEntity);

                        // Save changes to the database
                        dbContext.SaveChanges();
                    }

                    // Redirect to a success or result view
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Log the error, handle it, or display an error message to the user
                    ViewBag.ErrorMessage = $"An error occurred during registration: {ex.Message}";
                    return View();
                }
            }

            // If the model state is not valid, return the registration view with validation errors
            return View(userModel);
        }


        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult ForgetPassword()
        {
            return View();
        }


        //yeni eklemeler 

        [Authorize]
        [HttpGet]
        public ActionResult ShowAvailableAppointments(DateTime? currentDT, int? depart)
        {
            ViewBag.Sehirs = GetSehir(); // Populate branches for the dropdown
            ViewBag.Departments = GetSalons(); // Always populate departments for the dropdown
            ViewBag.EtkinlikDetay = GetEtkinlikDetay();
            ViewBag.Etkinliks = GetEtkinliks(); // Add this line to populate etkinliks

            TempData["selectedDate"] = Request.QueryString["currentDT"];
            TempData["selectedEtkinlik"] = Request.QueryString["Etkinlik"];
            TempData["selectedSehir"] = Request.QueryString["sehir"];
            TempData["selectedBranch"] = Request.QueryString["branch"];
            TempData["selectedDepartment"] = Request.QueryString["depart"];
            TempData["selectedTime"] = Request.QueryString["selectedTime"];


            TempData.Keep("selectedDate");
            TempData.Keep("selectedEtkinlik");
            TempData.Keep("selectedSehir");
            TempData.Keep("selectedBranch");
            TempData.Keep("selectedDepartment");

            TempData.Keep("selectedDate");
            TempData.Keep($"EtkinlikName_{TempData["selectedEtkinlik"]}");
            TempData.Keep($"SehirName_{TempData["selectedSehir"]}");
            TempData.Keep($"BranchName_{TempData["selectedBranch"]}");
            TempData.Keep($"DepartName_{TempData["selectedDepartment"]}");
            TempData.Keep("selectedTime");

            // If values are not provided, show the form
            if (!currentDT.HasValue || !depart.HasValue)
            {
                return View();
            }

            var result = new List<Appointment>();

            if (depart == 2)
            {
                result = _context.Database.SqlQuery<Appointment>(
                    "EXEC SelectProcedure @currentDT, @depart",
                    new SqlParameter("@currentDT", currentDT.Value),
                    new SqlParameter("@depart", depart.Value)
                ).ToList();
            }
            else if (depart == 3)
            {
                result = _context.Database.SqlQuery<Appointment>(
                    "EXEC ktProcedure @currentDT, @depart",
                    new SqlParameter("@currentDT", currentDT.Value),
                    new SqlParameter("@depart", depart.Value)
                ).ToList();
            }
            else if (depart == 1)
            {
                result = _context.Database.SqlQuery<Appointment>(
                    "EXEC TiyatroProcedure @currentDT, @depart",
                    new SqlParameter("@currentDT", currentDT.Value),
                    new SqlParameter("@depart", depart.Value)
                ).ToList();
            }

            // Process the result as needed

            return View(result);
        }

        [HttpGet]
        public ActionResult GetSalonsBySehir(int branchId)
        {
            var salons = GetSalonsBySehirFromDatabase(branchId); // Replace with your logic to get departments based on branchId
            var salonList = salons.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Depart }).ToList();
            return Json(salonList, JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<salon> GetSalonsBySehirFromDatabase(int branchId)
        {
            // Replace this with your logic to get departments based on branchId from your database
            // For example, you might query your database using Entity Framework or another data access method.
            return _context.Set<salon>().Where(s => s.SubeId == branchId).ToList();
        }





        // al sehirleri 

        [HttpGet]
        public ActionResult GetetkinlikdetayByEtkinlik(int etkinlikId)
        {
            var etkinlikdetays = GetetkinlikdetayByEtkinlikFromDatabase(etkinlikId);
            var etkinlikdetaylist = etkinlikdetays.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name }).ToList();
            return Json(etkinlikdetaylist, JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<etkinlikdetay> GetetkinlikdetayByEtkinlikFromDatabase(int etkinlikId)
        {
            return _context.Set<etkinlikdetay>().Where(s => s.EtkinlikId == etkinlikId).ToList();
        }






        [HttpGet]
        public ActionResult GetsehirByEtkinlikDetay(int sehirId)
        {
            var sehirs = GetsehirByEtkinlikDetayFromDatabase(sehirId);
            var sehirList = sehirs.Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.Name }).ToList();
            return Json(sehirList, JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<sehir> GetsehirByEtkinlikDetayFromDatabase(int sehirId)
        {
            // Replace this with your logic to get branches based on sehirId from your database
            return _context.Set<sehir>().Where(b => b.SehirId == sehirId).ToList();
        }







        private SelectList GetSehir()
        {
            var sehirs = _context.Set<sehir>().ToList();

            foreach (var sehir in sehirs)
            {
                TempData[$"SehirName_{sehir.Id}"] = sehir.Name;
            }

            return new SelectList(sehirs, "Id", "Name");
        }



        private SelectList GetSalons()
        {
            var salons = _context.Set<salon>().ToList();

            foreach (var salon in salons)
            {
                TempData[$"SalonName_{salon.Id}"] = salon.Depart;
            }

            return new SelectList(salons, "Id", "Depart");
        }

        private SelectList GetEtkinliks()
        {
            var etkinliks = _context.Set<Etkinlik>().ToList();

            foreach (var etkinlik in etkinliks)
            {
                TempData[$"EtkinlikName_{etkinlik.Id}"] = etkinlik.Name;
            }

            return new SelectList(etkinliks, "Id", "Name");
        }

        private SelectList GetEtkinlikDetay()
        {
            var etkinlikdetays = _context.Set<etkinlikdetay>().ToList();

            foreach (var etkinlikdetay in etkinlikdetays)
            {
                TempData[$"EtkinlikDetay_{etkinlikdetay.Id}"] = etkinlikdetay.Name;
            }

            return new SelectList(etkinlikdetays, "Id", "Name");
        }



        [HttpGet]
        public ActionResult OtherPage()
        {
            // Fetch orders with non-empty specified properties
            var ordersWithSameProps = _context.Orderss
                .Where(o =>
                    !string.IsNullOrEmpty(o.Category) &&
                    !string.IsNullOrEmpty(o.TicketName) &&
                    !string.IsNullOrEmpty(o.City) &&
                    !string.IsNullOrEmpty(o.Salon) &&
                    !string.IsNullOrEmpty(o.Seat) &&
                    !string.IsNullOrEmpty(o.TicketDate) &&
                    !string.IsNullOrEmpty(o.TicketHour))
                .ToList();

            // Extract relevant information and pass it to the view
            var relevantData = ordersWithSameProps
                .Select(o => new
                {
                    // Adjust the property names based on your actual model
                    Category = o.Category,
                    TicketName = o.TicketName,
                    City = o.City,
                    Salon = o.Salon,
                    Seat = o.Seat,
                    TicketDate = o.TicketDate,
                    TicketHour = o.TicketHour
                })
                .ToList();

            // Pass the relevant data to the view
            ViewBag.OrdersWithSameProps = relevantData;

            // Fetch orders with occupied seats from the database based on the relevant data
            var ordersWithOccupiedSeats = ordersWithSameProps
                .Where(o => !string.IsNullOrEmpty(o.Seat))
                .ToList();

            // Extract occupied seat numbers from orders
            var occupiedSeatNumbers = ordersWithOccupiedSeats
                .SelectMany(o => o.Seat.Split(',').Select(s => int.Parse(s.Trim())))
                .ToList();

            // Pass the occupied seat information to the view
            ViewBag.OccupiedSeatNumbers = occupiedSeatNumbers;

            return View();
        }
        [HttpPost]
        public ActionResult VerifySeats()
        {
            // Your verification logic here
            // Example: Check if the selected data matches any existing order
            var ordersWithSameProps = _context.Orderss
           .Where(o => !string.IsNullOrEmpty(o.Category) &&
                       !string.IsNullOrEmpty(o.TicketName) &&
                       !string.IsNullOrEmpty(o.City) &&
                       !string.IsNullOrEmpty(o.Salon) &&
                       !string.IsNullOrEmpty(o.Seat) &&
                       !string.IsNullOrEmpty(o.TicketDate) &&
                       !string.IsNullOrEmpty(o.TicketHour))
           .ToList();

            // Extract relevant information and pass it to the view
            var relevantData = ordersWithSameProps
                .Select(o => new
                {
                    // Adjust the property names based on your actual model
                    Category = o.Category,
                    TicketName = o.TicketName,
                    City = o.City,
                    Salon = o.Salon,
                    Seat = o.Seat,
                    TicketDate = o.TicketDate,
                    TicketHour = o.TicketHour
                })
                .ToList();

            // Pass the relevant data to the view
            ViewBag.OrdersWithSameProps = relevantData;



            return View("OtherPage");
        }
        [HttpPost]
        public ActionResult CreatePayment(Orders newOrder, string selectedDate, string selectedEtkinlik, string selectedSehir, string selectedBranch, string selectedDepartment, string selectedTime, string selectedSeatsString)
        {
            if (ModelState.IsValid)
            {
                using (var context = new DataContext())
                {
                    // Yalnızca belirli bilgileri al ve yeni bir Orders nesnesi oluştur
                    var partialOrder = new Orders
                    {
                        UserId = 1,
                        UserName = @User.Identity.Name,
                        Category = selectedEtkinlik,
                        TicketName = selectedSehir,
                        City = selectedBranch,
                        Salon = selectedDepartment,
                        Seat = selectedSeatsString,
                        TicketDate = selectedDate,
                        TicketHour = selectedTime


                        // Diğer özellikleri de ekleyebilirsiniz
                    };

                    context.Orderss.Add(partialOrder);
                    context.SaveChanges();
                }

                // Diğer işlemler

                return RedirectToAction("Index");
            }
            return View(newOrder);
        }

        //ödeme alıncak.
        public ActionResult Payment()
        {
            return View();
        }



        public ActionResult MyTickets()
        {
            // Get the current user's identity name
            var currentUserName = User.Identity.Name;

            // Retrieve orders for the current user from the Orders table
            var userOrders = _context.Orderss.Where(o => o.UserName == currentUserName && o.Salon.Length >= 3).ToList();

            // Pass the list of orders to the view
            return View(userOrders);
        }




        public async Task<ActionResult> GetUserByUsername(string userName)
        {
            var user = await _context.Set<User>().SingleOrDefaultAsync(u => u.UserName == userName);

            if (user != null)
            {
                // Pass the user object to the view
                return View(user);
            }
            else
            {
                // Handle user not found case
                return View("UserNotFound");
            }
        }
    }
}