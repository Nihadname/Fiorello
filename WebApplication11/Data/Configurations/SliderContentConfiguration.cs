using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication11.Models;

namespace WebApplication11.Data.Configurations
{
    public class SliderContentConfiguration : IEntityTypeConfiguration<SliderContent>
    {
        public void Configure(EntityTypeBuilder<SliderContent> builder)
        {
            builder.Property(o => o.Title).IsRequired().HasMaxLength(50);
            builder.Property(o=>o.Desc).IsRequired().HasMaxLength(160);
            builder.Property(o=>o.SignImageUrl).HasMaxLength(500).IsRequired();
        }
    }
}
