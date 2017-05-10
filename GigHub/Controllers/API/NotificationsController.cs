using AutoMapper;
using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.API
{
    public class NotificationsController : ApiController
    {
        private ApplicationDbContext _context;

        public NotificationsController()
        {
            _context = new ApplicationDbContext();
        }
        public IEnumerable<NotificationDto> GetNewNotifications()
        {
            var userId = User.Identity.GetUserId();

            var notifications = _context.UserNotifications
                .Where(un => un.UserId == userId && un.IsRead == false)
                .Select(un => un.Notification)
                .Include(u => u.Gig.Artist)
                .Include(n => n.Gig)
                .ToList();

            //Mapper.Map<List<Notification>, List<NotificationDto>>(notifications);
            //Mapper.Map<List<Notification>, List<NotificationDto>>(notifications);
            var result = notifications.Select(Mapper.Map<Notification, NotificationDto>);

            return result;
        }


        [HttpPost]
        public IHttpActionResult MarkAsRead()
        {
            var userId = User.Identity.GetUserId();

            var currentUsernotifications = _context.UserNotifications
                .Where(un => un.UserId == userId)
                .ToList();

            currentUsernotifications.ForEach(n => n.Read());

            _context.SaveChanges();

            return Ok();

        }

    }

    /*
     return notifications.Select(n => new NotificationDto()
                {
                    DateTime = n.DateTime,
                    Gig = new GigDto()
                    {
                        Artist = new UserDto()
                        {
                           Id = n.Gig.Artist.Id,
                           Name = n.Gig.Artist.Name
                        },
                        DateTime = n.Gig.DateTime,
                        Id = n.Gig.Id,
                        IsCanceled = n.Gig.IsCanceled,
                        Venue = n.Gig.Venue

                    },
                    OriginalDateTime = n.OriginalDateTime,
                    OriginalVenue = n.OriginalVenue,
                    Type = n.Type

                })
     * */
}

