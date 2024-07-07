using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication11.Models;

namespace WebApplication11.Data.Configurations
{
    public class SettingConfiguration : IEntityTypeConfiguration<Setting>
    {
        public void Configure(EntityTypeBuilder<Setting> builder)
        {
            builder.Property(s => s.Key)
                   .IsRequired();

            builder.HasIndex(s => s.Key)
                   .IsUnique();
        }
    }
}
