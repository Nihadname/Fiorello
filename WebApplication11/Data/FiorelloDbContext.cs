using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using WebApplication11.Data.Configurations;
using WebApplication11.Models;
using WebApplication11.Models.Common;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WebApplication11.Data
{
    public class FiorelloDbContext :IdentityDbContext<AppUser>
    {
        public DbSet<Slider> sliders { get; set; }
        public DbSet<SliderContent> contents { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Setting> settings { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<ProductImage> productsImage { get; set; }
        public DbSet<About> abouts { get; set; }
        public DbSet<AboutDetail> aboutDetails { get; set; }
        public DbSet<Expert> experts { get; set; }
        public DbSet<ExpertItem> expertItems { get; set; }
        public DbSet<Blog> blogs { get; set; }
        public FiorelloDbContext(DbContextOptions<FiorelloDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // modelBuilder.ApplyConfiguration(new SliderContentConfiguration());
            // modelBuilder.ApplyConfiguration(new SliderConfiguration());
            // modelBuilder.ApplyConfiguration(new ProductConfiguration());
            // modelBuilder.ApplyConfiguration(new AboutConfiguration());
            // modelBuilder.ApplyConfiguration(new ExpertConfiguration());
            // modelBuilder.ApplyConfiguration(new ExpertItemConfiguration());
            //modelBuilder.ApplyConfiguration(new SettingConfiguration());
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            modelBuilder.Entity<Blog>()
           .Property(o => o.DateTime)
           .HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<AppUser>().HasKey(s=>s.Id);
        }
    }
}
