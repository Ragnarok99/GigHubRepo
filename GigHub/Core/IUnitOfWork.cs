using GigHub.Core.Repositories;

namespace GigHub.Core
{
    public interface IUnitOfWork
    {
        IGigRepository Gigs { get; }
        IFollowingRepository Followings { get; }
        IGenreRepository Genres { get; }
        IAttendanceRepository Attendances { get; }
        void Complete();
    }
}