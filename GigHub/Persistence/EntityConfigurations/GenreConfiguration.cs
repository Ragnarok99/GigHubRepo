using GigHub.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace GigHub.Persistence.EntityConfigurations
{
    public class GenreConfiguration : EntityTypeConfiguration<Genre>
    {
        public GenreConfiguration()
        {
            Property(ge => ge.Name)
                .IsRequired();


            Property(ge => ge.Name)
                .HasMaxLength(255);
        }
    }
}