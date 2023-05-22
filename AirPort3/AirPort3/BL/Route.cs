using Microsoft.AspNetCore.Mvc.TagHelpers;
using System.Collections;
using System.Text.Json;

namespace AirPort.BL {
    public class Route : IEnumerable<Station>, IEnumerable {
        Dictionary<Station, List<Station>> route = new Dictionary<Station, List<Station>>();

        public int FlightCount = 0;

        public void Add(Station station) {
            if (route.ContainsKey(station))
                return;
            route.Add(station, new List<Station>());
        }


        public void ConnectToStation(Station from, Station to) {
            if (!route.ContainsKey(from) || !route.ContainsKey(to))
                throw new Exception("this station is not exists!");
            route[from].Add(to);
        }


        public List<Station> GetNext(Station station) {
            return route[station];
        }

        public void AddMany(params Station[] stations) {
            foreach (Station station in stations) {
                if (route.ContainsKey(station))
                    return;
                route.Add(station, new List<Station>());
            }
        }
        public void ConnectManyToStation(Station from, params Station[] stations) {
            if (!route.ContainsKey(from))
                throw new Exception("this station is not exists!");
            foreach (Station station in stations) {
                if (!route.ContainsKey(station))
                    throw new Exception("this station is not exists!");
                route[from].Add(station);
            }
        }

        public IEnumerator<Station> GetEnumerator() {
            return route.Keys.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return route.Keys.GetEnumerator();
        }
    }
}
