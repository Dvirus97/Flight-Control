using AirPort.DB;
using AirPort.SignalR;
using AirPort.DTOs;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace AirPort.BL {
    public class RouteProvider {


        Route landing = new();
        Route departure = new();

        public Route GetLanding => landing;
        public Route GetDeparture => departure;


        JsonData? ReadJson() {
            var path = Path.Combine(Environment.CurrentDirectory, "StationsConfiguration.json");
            var text = File.ReadAllText(path);
            return text.FromJson<JsonData>();
        }


        Station Get(int id) => stations.First(x => x.Id == id);

        List<Station> stations = new();
        private readonly IHubContext<AirPortHub> hub;

        public RouteProvider(IHubContext<AirPortHub> hub) {
            this.hub = hub;

            JsonData? data = ReadJson();
            if (data == null || data.Stations == null || data.Edges == null) {
                BackUpInit();
                return;
            }

            stations.AddRange(data.Stations.Select(x => new Station(hub) { Id = x.Id, WaitTime = TimeSpan.FromSeconds(x.WaitTime) }));

            foreach (var item in data.Edges) {
                var from = Get(item.fromId);
                var to = Get(item.toId);

                if (item.state == "l") {
                    landing.Add(from);
                    landing.Add(to);
                    landing.ConnectToStation(from, to);
                }
                else if (item.state == "d") {
                    departure.Add(from);
                    departure.Add(to);
                    departure.ConnectToStation(from, to);
                }
            }
        }

        void BackUpInit() {

            var s0 = new Station(hub) { Id = 0, WaitTime = TimeSpan.FromSeconds(1) };
            var s1 = new Station(hub) { Id = 1, WaitTime = TimeSpan.FromSeconds(1) };
            var s2 = new Station(hub) { Id = 2, WaitTime = TimeSpan.FromSeconds(1) };
            var s3 = new Station(hub) { Id = 3, WaitTime = TimeSpan.FromSeconds(2) };
            var s4 = new Station(hub) { Id = 4, WaitTime = TimeSpan.FromSeconds(3) };
            var s5 = new Station(hub) { Id = 5, WaitTime = TimeSpan.FromSeconds(1) };
            var s6 = new Station(hub) { Id = 6, WaitTime = TimeSpan.FromSeconds(2) };
            var s7 = new Station(hub) { Id = 7, WaitTime = TimeSpan.FromSeconds(2) };
            var s8 = new Station(hub) { Id = 8, WaitTime = TimeSpan.FromSeconds(2) };
            var s9 = new Station(hub) { Id = 9, WaitTime = TimeSpan.FromSeconds(2) };

            var s100 = new Station(hub) { Id = 100, WaitTime = TimeSpan.FromSeconds(2) };
            var t1 = new Station(hub) { Id = 404, WaitTime = TimeSpan.FromSeconds(0) };

            landing.AddMany(s0, s1, s2, s3, s4, s5, s6, s7, t1);

            landing.ConnectToStation(s0, s1);
            landing.ConnectToStation(s1, s2);
            landing.ConnectToStation(s2, s3);
            landing.ConnectToStation(s3, s4);
            landing.ConnectToStation(s4, s5);
            landing.ConnectManyToStation(s5, s6, s7);

            landing.ConnectToStation(s6, t1);
            landing.ConnectToStation(s7, t1);

            departure.AddMany(s100, s7, s8, s4, s9, t1);

            departure.ConnectManyToStation(s100, s7);
            departure.ConnectToStation(s7, s8);
            departure.ConnectToStation(s8, s4);
            departure.ConnectToStation(s4, s9);
            departure.ConnectToStation(s9, t1);
        }


        //public RouteProvider(IHubContext<FlightHubhub{

        //    var s0 = new Station(hub) { Id = 0, WaitTime = TimeSpan.FromSeconds(1) };
        //    var s1 = new Station(hub) { Id = 1, WaitTime = TimeSpan.FromSeconds(1) };
        //    var s2 = new Station(hub) { Id = 2, WaitTime = TimeSpan.FromSeconds(1) };
        //    var s3 = new Station(hub) { Id = 3, WaitTime = TimeSpan.FromSeconds(2) };
        //    var s4 = new Station(hub) { Id = 4, WaitTime = TimeSpan.FromSeconds(3) };
        //    var s5 = new Station(hub) { Id = 5, WaitTime = TimeSpan.FromSeconds(1) };
        //    var s6 = new Station(hub) { Id = 6, WaitTime = TimeSpan.FromSeconds(2) };
        //    var s7 = new Station(hub) { Id = 7, WaitTime = TimeSpan.FromSeconds(2) };
        //    var s8 = new Station(hub) { Id = 8, WaitTime = TimeSpan.FromSeconds(2) };
        //    var s9 = new Station(hub) { Id = 9, WaitTime = TimeSpan.FromSeconds(2) };

        //    var s100 = new Station(hub) { Id = 100, WaitTime = TimeSpan.FromSeconds(2) };
        //    var t1 = new Station(hub) { Id = 404, WaitTime = TimeSpan.FromSeconds(0) };

        //    var a = TimeSpan.FromSeconds();

        //    //// terminal

        //    AllStation.AddRange(new[] { s1, s2, s3, s4, s5, s6, s7, s8, s9 });


        //    //landing 
        //    {
        //        landing.AddMany(s0, s1, s2, s3, s4, s5, s6, s7, t1);

        //        landing.ConnectToStation(s0, s1);
        //        landing.ConnectToStation(s1, s2);
        //        landing.ConnectToStation(s2, s3);
        //        landing.ConnectToStation(s3, s4);
        //        landing.ConnectToStation(s4, s5);
        //        landing.ConnectManyToStation(s5, s6, s7);

        //        landing.ConnectToStation(s6, t1);
        //        landing.ConnectToStation(s7, t1);
        //    }

        //    // deparcher
        //    {

        //        departure.AddMany(s100, s7/*, s6*/, s8, s4, s9, t1);
        //        //departure.Add(s100);
        //        //departure.Add(s6);
        //        //departure.Add(s7);
        //        //departure.Add(s8);
        //        //departure.Add(s4);
        //        //departure.Add(s9);
        //        //departure.Add(t1);

        //        departure.ConnectManyToStation(s100, s7/*, s6*/);
        //        departure.ConnectToStation(s7, s8);
        //        //departure.ConnectToStation(s7, s8);
        //        departure.ConnectToStation(s8, s4);
        //        departure.ConnectToStation(s4, s9);
        //        departure.ConnectToStation(s9, t1);
        //    }

        //    landing.Save();

        //}


    }
}
