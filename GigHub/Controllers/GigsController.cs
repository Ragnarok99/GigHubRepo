using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Core.ViewModels;
using GigHub.Persistence;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public GigsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;


        }

        [Authorize]
        public ActionResult Details(int Id)
        {
            var gig = new ApplicationDbContext().Gigs
                .Include(g => g.Artist)
                .Single(g => g.Id == Id);

            if (gig == null)
                return HttpNotFound();

            var gigDetailsViewModel = new GigDetailsViewModel()
            {
                Gig = gig
            };

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();

                gigDetailsViewModel.FollowingArtist = _unitOfWork.Followings.GetFollowing(userId, gig.ArtistId) != null;

                gigDetailsViewModel.AttendingGig = _unitOfWork.Attendances.GetAttendance(gig.Id, userId) != null;

            }

            return View(gigDetailsViewModel);
        }

        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _unitOfWork.Gigs.GetUpcomingGigsByArtist(userId);

            return View(gigs);

        }
        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();



            var viewModel = new GigsViewModel()
            {
                UpcomingGigs = _unitOfWork.Gigs.GetGigsUserAttending(userId),
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs a los que asistiré",
                Attendances = _unitOfWork.Attendances.GetFutureAttendances(userId).ToLookup(a => a.GigId)
            };

            return View("gigs", viewModel);
        }




        [Authorize]
        public ActionResult Edit(int gigId)
        {
            var gig = _unitOfWork.Gigs.GetGig(gigId);
            //var genresToDisplay = new List<Genre>
            //{
            //    new Genre()
            //    {
            //        Id = 0,
            //        Name = "[Select a Genre]"
            //    }
            //};

            //foreach (var genre in _context.Genres.ToList())
            //{
            //    genresToDisplay.Add(genre);
            //}

            if (gig == null)
                return HttpNotFound();

            if (gig.ArtistId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

            var viewModel = new GigFormViewModel()
            {
                Id = gig.Id,
                Genres = _unitOfWork.Genres.getGenres(),
                Date = gig.DateTime.ToString("d MMM yyyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                Venue = gig.Venue,
                Genre = gig.GenreId,
                Heading = "Edit a Gig"
            };
            return View("GigForm", viewModel);
        }


        [Authorize]
        public ActionResult Create()
        {
            var genresToDisplay = new List<Genre>
            {
                new Genre()
                {
                    Id = 0,
                    Name = "[Select a Genre]"
                }
            };
            var genres = _unitOfWork.Genres.getGenres();

            foreach (var genre in genres)
            {
                genresToDisplay.Add(genre);
            }


            var viewModel = new GigFormViewModel()
            {
                Genres = genresToDisplay,
                Heading = "Add a Gig"
            };
            return View("GigForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _unitOfWork.Genres.getGenres();
                return View("GigForm", viewModel);
            }

            var gig = new Gig()
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = viewModel.DateTime,
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
            };

            _unitOfWork.Gigs.AddGig(gig);
            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Gigs");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _unitOfWork.Genres.getGenres();
                return View("GigForm", viewModel);
            }
            var gig = _unitOfWork.Gigs.GetGigWithAttendies(viewModel.Id);

            if (gig == null)
                return HttpNotFound();

            if (gig.ArtistId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

            gig.Modify(viewModel.DateTime, viewModel.Venue, viewModel.Genre);

            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Gigs");
        }

        public ActionResult Search(GigsViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new { query = viewModel.SearchTerm });
        }
    }

}