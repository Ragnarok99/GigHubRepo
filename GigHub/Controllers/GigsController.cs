﻿using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;


        public GigsController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize]
        public ActionResult Details(int Id)
        {
            var gig = _context.Gigs
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

                gigDetailsViewModel.FollowingArtist = _context.Followings
                    .Any(f => f.FollowerId == userId && f.FolloweeId == gig.ArtistId);

                gigDetailsViewModel.AttendingGig = _context.Attendances
                    .Any(a => a.GigId == Id && a.AttendeeId == userId);

            }

            return View(gigDetailsViewModel);
        }

        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _context.Gigs
                .Where(g => g.ArtistId == userId && g.DateTime > DateTime.Now && !g.IsCanceled)
                .Include(g => g.Genre)
                .ToList();



            return View(gigs);

        }
        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();

            var gigs = _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(g => g.Genre)
                .Include(g => g.Artist)
                .ToList();

            var attendances = _context.Attendances
            .Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now)
            .ToList()
            .ToLookup(a => a.GigId);

            var viewModel = new GigsViewModel()
            {
                UpcomingGigs = gigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs a los que asistiré",
                Attendances = attendances
            };
            return View("gigs", viewModel);
        }
        [Authorize]
        public ActionResult Edit(int? gigId)
        {
            var userId = User.Identity.GetUserId();

            var gig = _context.Gigs.Single(g => g.Id == gigId && g.ArtistId == userId);

            var genresToDisplay = new List<Genre>
            {
                new Genre()
                {
                    Id = 0,
                    Name = "[Select a Genre]"
                }
            };

            foreach (var genre in _context.Genres.ToList())
            {
                genresToDisplay.Add(genre);
            }


            var viewModel = new GigFormViewModel()
            {
                Id = gig.Id,
                Genres = genresToDisplay,
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

            foreach (var genre in _context.Genres.ToList())
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
                viewModel.Genres = _context.Genres.ToList();
                return View("GigForm", viewModel);
            }

            var gig = new Gig()
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = viewModel.DateTime,
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
            };

            _context.Gigs.Add(gig);
            _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("GigForm", viewModel);
            }
            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs.Include(g => g.Attendances.Select(a => a.Attendee))
                .Single(g => g.Id == viewModel.Id && g.ArtistId == userId);

            gig.Modify(viewModel.DateTime, viewModel.Venue, viewModel.Genre);

            _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }

        public ActionResult Search(GigsViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new { query = viewModel.SearchTerm });
        }
    }

}