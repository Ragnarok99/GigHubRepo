using GigHub.Core.Models;
using System.Collections.Generic;

namespace GigHub.Core.Repositories
{
    public interface IFollowingRepository
    {
        Following GetFollowing(string userId, string gigArtistId);
        IEnumerable<ApplicationUser> GetArtistsUserFollowing(string userId);
    }
}