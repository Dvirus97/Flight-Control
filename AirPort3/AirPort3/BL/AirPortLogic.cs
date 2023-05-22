using AirPort.SignalR;
using AirPort.DTOs;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using AirPort3.DTOs;
using AirPort.DB.Models;
using AirPort3.DB.Repositories;
using AirPort3.DTOs.UI;

namespace AirPort.BL {
    public class AirPortLogic {
        private readonly IHubContext<AirPortHub> hub;
        private readonly IRepository<FlightDb> repository;
        public ConcurrentBag<Flight> ActiveFlights = new();
        public AirPortLogic(IHubContext<AirPortHub> hub, IRepository<FlightDb> repository) {
            this.hub = hub;
            this.repository = repository;
        }

        public async Task AddFlight(int num, Route route, FlightState state) {

            Flight? f = new(num, state == FlightState.Landing, state);

            ActiveFlights.Add(f);
            await SendFlightToClient();

            //Console.WriteLine($"{f.Id} is added to the field");

            f.DBFlight.EnterTime = DateTime.Now;
            Interlocked.Increment(ref route.FlightCount);
            await f.Run(route);
            Interlocked.Decrement(ref route.FlightCount);
            f.DBFlight.ExitTime = DateTime.Now;

            repository.Add(f.DBFlight);
            if (ActiveFlights.TryTake(out f)) {
                await SendFlightToClient();
            }
            //Console.WriteLine("After Save");
        }

        async Task SendFlightToClient() {
            var flights = ActiveFlights.Select(x => new FlightUI(x.Id, x.State.ToString()));
            await hub.Clients.All.SendAsync("GetFlights", flights.ToJson());
        }
    }
}
