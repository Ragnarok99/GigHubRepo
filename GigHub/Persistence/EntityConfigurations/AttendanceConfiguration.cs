using GigHub.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace GigHub.Persistence.EntityConfigurations
{
    public class AttendanceConfiguration : EntityTypeConfiguration<Attendance>
    {
        public AttendanceConfiguration()
        {
            HasKey(at => at.GigId);

            Property(at => at.GigId)
                .HasColumnOrder(1);

            HasKey(at => at.AttendeeId);

            Property(at => at.AttendeeId).
                HasColumnOrder(2);


        }
    }
}