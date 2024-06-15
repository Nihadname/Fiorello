using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication11.Models;

namespace WebApplication11.Data.Configurations
{
    public class ExpertConfiguration : IEntityTypeConfiguration<Expert>
    {
        public void Configure(EntityTypeBuilder<Expert> builder)
        {
           foreach( var properties in typeof(Expert).GetProperties().Where(p => p.PropertyType == typeof(string)))
            {
                builder.Property(properties.Name).IsRequired();
            }
        }
    }
}
