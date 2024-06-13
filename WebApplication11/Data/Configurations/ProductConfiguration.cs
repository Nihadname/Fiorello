using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication11.Models;

namespace WebApplication11.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(o => o.Name).IsRequired().HasMaxLength(60);
            builder.Property(o=>o.Price).IsRequired().HasPrecision(18,2);
        }
    }
}
