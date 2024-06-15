using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication11.Models;

namespace WebApplication11.Data.Configurations
{
    public class ExpertItemConfiguration : IEntityTypeConfiguration<ExpertItem>
    {
        public void Configure(EntityTypeBuilder<ExpertItem> builder)
        {
            foreach (var property in typeof(ExpertItem).GetProperties().Where(p => p.PropertyType == typeof(string)))
            {
                builder.Property(property.Name).IsRequired();
            }

        }
    }
}
