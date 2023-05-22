using AirPort3.DTOs;

namespace AirPort3.DTOs.UI {
    public class FlightUI {
        public int flightId { get; set; }
        public string state { get; set; }

        public FlightUI(int flightId, string state) {
            this.flightId = flightId;
            this.state = state;
        }
    }
}
