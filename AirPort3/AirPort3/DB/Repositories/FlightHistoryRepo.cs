using AirPort.DB;
using AirPort.DB.Models;

namespace AirPort3.DB.Repositories {
    public class FlightHistoryRepo : IRepository<FlightDb> {
        private readonly AirPortContext context;

        public FlightHistoryRepo(AirPortContext context) {
            this.context = context;
        }

        public void Add(FlightDb entity) {
            context.Flights.Add(entity);
            Save();
        }

        public FlightDb? Get(int id) {
            return GetAll().SingleOrDefault(x => id == x.Id);
        }

        public IEnumerable<FlightDb> GetLast(int number) {
            return GetAll()
                .OrderByDescending(f => f.EnterTime)
                .Take(number)
                .Reverse();
        }

        public IQueryable<FlightDb> GetAll() {
            return context.Flights;
        }

        int Save() => context.SaveChanges();
    }
}
