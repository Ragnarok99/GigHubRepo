using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.API
{

    [Authorize]
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _dbContext;

        public AttendancesController()
        {
            _dbContext = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {

            var userId = User.Identity.GetUserId();
            var exist = _dbContext.Attendances.Any(a => a.AttendeeId == userId && a.GigId == dto.GigId);
            if (exist) return BadRequest("The attendace already exist");

            var attendance = new Attendance()
            {
                GigId = dto.GigId,
                AttendeeId = userId
            };

            _dbContext.Attendances.Add(attendance);
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Attend(int id)
        {

            var userId = User.Identity.GetUserId();
            var attendance = _dbContext.Attendances.Single(a => a.AttendeeId == userId && a.GigId == id);

            if (attendance == null)
                return NotFound();

            _dbContext.Attendances.Remove(attendance);
            _dbContext.SaveChanges(); 



            return Ok(id);
        }
    }
}
