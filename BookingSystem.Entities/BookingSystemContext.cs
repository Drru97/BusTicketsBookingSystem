using System.Data.Entity;

namespace BookingSystem.Entities
{
    public class BookingSystemContext : DbContext
    {
        public BookingSystemContext() : base("name=BusTicketsBookingSystemDB") { }

        public virtual DbSet<Bus> Bus { get; set; }
        public virtual DbSet<Driver> Driver { get; set; }
        public virtual DbSet<Journey> Journey { get; set; }
        public virtual DbSet<Passenger> Passenger { get; set; }
        public virtual DbSet<Route> Route { get; set; }
        public virtual DbSet<RoutePoint> RoutePoint { get; set; }
        public virtual DbSet<Ticket> Ticket { get; set; }
        public virtual DbSet<Traffic> Traffic { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bus>()
                .HasMany(e => e.Journey)
                .WithRequired(e => e.Bus)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Driver>()
                .HasMany(e => e.Journey)
                .WithRequired(e => e.Driver)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Journey>()
                .HasMany(e => e.Ticket)
                .WithRequired(e => e.Journey)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Journey>()
                .HasMany(e => e.Traffic)
                .WithRequired(e => e.Journey)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Passenger>()
                .HasMany(e => e.Ticket)
                .WithRequired(e => e.Passenger)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Route>()
                .HasMany(e => e.Journey)
                .WithRequired(e => e.Route)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RoutePoint>()
                .HasMany(e => e.Route)
                .WithRequired(e => e.RoutePoint)
                .HasForeignKey(e => e.ArrivalPointId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RoutePoint>()
                .HasMany(e => e.Route1)
                .WithRequired(e => e.RoutePoint1)
                .HasForeignKey(e => e.DeparturePointId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ticket>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Ticket>()
                .HasMany(e => e.Traffic)
                .WithRequired(e => e.Ticket)
                .WillCascadeOnDelete(false);
        }
    }
}
