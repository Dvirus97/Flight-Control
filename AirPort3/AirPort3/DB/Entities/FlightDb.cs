using AirPort3.DTOs;

namespace AirPort.DB.Models {
    public class FlightDb {
        public int Id { get; set; }
        public int FlightId { get; set; }
        public DateTime EnterTime { get; set; }
        public DateTime ExitTime { get; set; }
        public string? State { get; set; }
    }


}
