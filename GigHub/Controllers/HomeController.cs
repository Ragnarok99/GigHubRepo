using GigHub.Models;
using GigHub.Repositories;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly AttendanceRepository _attendanceRepository;


        public HomeController()
        {
            _dbContext = new ApplicationDbContext();
            _attendanceRepository = new AttendanceRepository(_dbContext);
        }
        public ActionResult Index(string query = null)
        {
            var upcomingGigs = _dbContext.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => g.DateTime > DateTime.Now && !g.IsCanceled);

            if (!string.IsNullOrWhiteSpace(query))
            {
                upcomingGigs = upcomingGigs
                    .Where(g =>
                            g.Artist.Name.Contains(query) ||
                            g.Genre.Name.Contains(query) ||
                            g.Venue.Contains(query));

            }

            var userId = User.Identity.GetUserId();

            var attendances = _attendanceRepository.GetFutureAttendances(userId).ToLookup(a => a.GigId);

            var homeViewModel = new GigsViewModel()
            {
                UpcomingGigs = upcomingGigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Próximos Gigs",
                SearchTerm = query,
                Attendances = attendances
            };

            return View("gigs", homeViewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your about page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}