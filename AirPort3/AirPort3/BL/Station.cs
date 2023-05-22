using AirPort.DB;
using AirPort.DB.Models;
using AirPort.SignalR;
using AirPort.DTOs;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;
using System.Text.Json.Serialization;
using AirPort3.DTOs;
using AirPort3.DTOs.UI;

namespace AirPort.BL {
    public class Station {

        public int Id { get; set; }
        public Flight? Flight { get; set; }
        public TimeSpan WaitTime { get; internal set; }

        SemaphoreSlim _sem = new(1);

        private IHubContext<AirPortHub> hub;

        public Station(IHubContext<AirPortHub> hub) => this.hub = hub;


        public async Task<Station> Enter(Flight flight, CancellationTokenSource cts) {

            try {
                await _sem.WaitAsync(cts.Token);
                cts.Cancel();
                this.Flight = flight;
                //Util.CC($"Station [ {Id} ] - has flight [ {Flight.Id} ] ", ConsoleColor.Green);

                if (Id != 404 && Id != 100 && Id != 0) {
                    var json = new StationUI(Id, flight.Id, flight.State.ToString()).ToJson();
                    //Console.WriteLine(json);
                    await hub.Clients.All.SendAsync("GetStation", json);
                }
                return this;
            }
            catch (Exception e) {
                e.Message.PrintColor(ConsoleColor.Blue);
                //Util.CC("Flight: " + flight.Id, ConsoleColor.Blue);
                //Util.CC("station: " + this.Id.ToString(), ConsoleColor.Blue);
                return this;
            }
        }

        internal async Task Exit() {
            //Util.CC($"Station [ {Id} ] - flight [ {Flight?.Id} ] is leaving ! ", ConsoleColor.Green);
            this.Flight = null;
            if (Id != 404 && Id != 100) {
                var json = new StationUI(Id, 0, FlightState.None.ToString()).ToJson();
                //Console.WriteLine(json);
                await hub.Clients.All.SendAsync("GetStation", json);
            }
            _sem.Release();
        }
    }
}
