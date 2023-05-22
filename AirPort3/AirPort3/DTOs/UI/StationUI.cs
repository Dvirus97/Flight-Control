using AirPort3.DTOs;

namespace AirPort3.DTOs.UI {
    public class StationUI {
        public int stationId { get; set; }
        public int flightId { get; set; }
        public string flightState { get; set; }

        public StationUI(int stationId, int flightId, string flightState) {
            this.stationId = stationId;
            this.flightId = flightId;
            this.flightState = flightState;
        }
    }
}
