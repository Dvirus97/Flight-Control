using AirPort.DB.Models;
using Microsoft.EntityFrameworkCore;

namespace AirPort.DB {
    public class AirPortContext : DbContext {
        public AirPortContext(DbContextOptions<AirPortContext> o) : base(o) {

        }

        public DbSet<FlightDb> Flights { get; set; }
    }
}
