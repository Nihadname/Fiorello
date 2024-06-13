using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication11.Models;

namespace WebApplication11.Data.Configurations
{
    public class AboutConfiguration : IEntityTypeConfiguration<About>
    {
        public void Configure(EntityTypeBuilder<About> builder)
        {
            builder.Property(o=>o.Title).IsRequired().HasMaxLength(90);
            builder.Property(o => o.Description).IsRequired().HasMaxLength(400);
            builder.Property(o => o.ImageUrl).IsRequired().HasMaxLength(400);
        }
    }
}
