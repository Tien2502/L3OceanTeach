using BTL3_MVT.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace BTL3_MVT.Data
{
    public class PersonContext : DbContext
    {
        public PersonContext() { }
        public PersonContext(DbContextOptions<PersonContext> options) : base(options) { }

        public DbSet<Person> Persons { get; set; }
        public DbSet<City> Citys { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Ward> Wards { get; set; }
        public DbSet<Ethnicity> Ethnicity { get; set; }
        public DbSet<Work> Works { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasOne(x => x.Ward).WithMany()
                .HasForeignKey(x => x.WardId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Person>().HasOne(x => x.District).WithMany()
                .HasForeignKey(x => x.DistrictId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Person>().HasOne(x => x.City).WithMany()
                .HasForeignKey(x => x.CityId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Person>().HasOne(x => x.WorkWard).WithMany()
                .HasForeignKey(x => x.WorkWardId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Person>().HasOne(x => x.WorkDistrict).WithMany()
                .HasForeignKey(x => x.WorkDistrictId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Person>().HasOne(x => x.WorkCity).WithMany()
                .HasForeignKey(x => x.WorkCityId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
