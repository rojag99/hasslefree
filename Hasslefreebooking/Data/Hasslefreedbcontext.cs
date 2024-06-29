using Hasslefreebooking.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hasslefreebooking.Data
{
    public class Hasslefreedbcontext : DbContext
    {

        public Hasslefreedbcontext(DbContextOptions<Hasslefreedbcontext> options) : base(options)
        {

        }
        public DbSet<Flights> Flights { get; set; }
        public DbSet<Flightschedules> FlightSchedules { get; set; }
        public DbSet<Traineschedules> Trainschedules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the relationship between Flights and FlightSchedules
            modelBuilder.Entity<Flights>()
                .HasMany(f => f.Flightschedules) // A Flight can have many FlightSchedules
                .WithOne(fs => fs.Flights) // Each FlightSchedule belongs to one Flight
                .HasForeignKey(fs => fs.FlightID); // Define the foreign key

            modelBuilder.Entity<Flights>()
           .HasKey(f => f.FlightID);

            modelBuilder.Entity<Flightschedules>()
                .HasKey(fs => fs.ScheduleID);

            // Optionally configure relationships here using modelBuilder.Entity<T>()
            modelBuilder.Entity<Flightschedules>()
                .HasOne(fs => fs.Flights)
                .WithMany(f => f.Flightschedules)
                .HasForeignKey(fs => fs.FlightID);

            modelBuilder.Entity<Traineschedules>()
                .HasKey(ts => ts.ScheduleID); // Define ScheduleID as primary key

            modelBuilder.Entity<Traineschedules>()
                .HasOne(ts => ts.DepartureStationNavigation)
                .WithMany()
                .HasForeignKey(ts => ts.DepartureStation)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict); // Configure DepartureStation as foreign key to Stations

            modelBuilder.Entity<Traineschedules>()
                .HasOne(ts => ts.ArrivalStationNavigation)
                .WithMany()
                .HasForeignKey(ts => ts.ArrivalStation)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict); // Configure ArrivalStation as foreign key to Stations

            modelBuilder.Entity<Stations>()
                .HasKey(s => s.StationCode); // Define StationCode as primary key
        }
    }
}