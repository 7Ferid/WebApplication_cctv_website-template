using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication_cctv_website_template.Models;

namespace WebApplication_cctv_website_template.Configurations
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(256);
            builder.Property(x => x.ImagePath).IsRequired().HasMaxLength(1024);


            builder.HasOne(x=>x.Position).WithMany(x=>x.Teams).HasForeignKey(x=>x.PositionId).HasPrincipalKey(x=>x.Id).OnDelete(DeleteBehavior.Cascade);

        }

       
    }
}
