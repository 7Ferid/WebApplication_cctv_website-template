
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication_cctv_website_template.Models;

namespace WebApplication_cctv_website_template.Configurations
{
    public class PositionConfiguration : IEntityTypeConfiguration<Position>
    {
        public void Configure(EntityTypeBuilder<Position> builder)
        {

            builder.Property(x => x.Name).IsRequired().HasMaxLength(256);
        }
    }
}
