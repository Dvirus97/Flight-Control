namespace AirPort.DTOs {
    public class JsonData {
        public StationJson[]? Stations { get; set; }
        public EdgeJson[]? Edges { get; set; }
    }

    public class StationJson {
        public int Id { get; set; }
        public int WaitTime { get; set; }
    }

    public class EdgeJson {
        public string? state { get; set; }
        public int fromId { get; set; }
        public int toId { get; set; }
    }





}
