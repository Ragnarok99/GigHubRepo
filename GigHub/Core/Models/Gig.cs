using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GigHub.Core.Models
{
    public class Gig
    {
        public int Id { get; set; }

        public bool IsCanceled { get; private set; }

        public ApplicationUser Artist { get; set; }

        public string ArtistId { get; set; }

        public DateTime DateTime { get; set; }

        public string Venue { get; set; }

        public Genre Genre { get; set; }

        public int GenreId { get; set; }


        public ICollection<Attendance> Attendances { get; private set; } = new Collection<Attendance>();

        public void Cancel()
        {
            IsCanceled = true;

            var notification = Notification.GigCanceled(this);

            foreach (var attendee in Attendances.Select(a => a.Attendee))
            {
                attendee.Notify(notification);

            }

        }

        public void Modify(DateTime viewModelDateTime, string viewModelVenue, int viewModelGenre)
        {
            var notification = Notification.GigUpdated(this, DateTime, Venue);

            DateTime = viewModelDateTime;
            Venue = viewModelVenue;
            GenreId = viewModelGenre;

            foreach (var attendee in Attendances.Select(a => a.Attendee))
            {
                attendee.Notify(notification);

            }

        }
    }



}