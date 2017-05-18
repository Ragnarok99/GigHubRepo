using GigHub.Core.Models;

namespace GigHub.Core.ViewModels
{
    public class GigDetailsViewModel
    {

        public Gig Gig { get; set; }
        public bool FollowingArtist { get; set; }
        public bool AttendingGig { get; set; }
    }
}