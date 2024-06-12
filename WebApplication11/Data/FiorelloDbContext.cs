using Microsoft.EntityFrameworkCore;
using System;
using WebApplication11.Data.Configurations;
using WebApplication11.Models;

namespace WebApplication11.Data
{
    public class FiorelloDbContext:DbContext
    {
       public DbSet<Slider> sliders {  get; set; }
         public   DbSet<SliderContent> contents { get; set; }
        public DbSet<Category> categories { get; set; }


        public FiorelloDbContext(DbContextOptions<FiorelloDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SliderContentConfiguration());
            modelBuilder.ApplyConfiguration(new SliderConfiguration());
        }
    }
}
