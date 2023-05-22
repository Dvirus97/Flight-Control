using AirPort.DB.Models;
using AirPort.SignalR;
using AirPort3.DTOs;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.SignalR;
using System.Security.Cryptography.Xml;

namespace AirPort.BL {
    public class Flight {
        public int Id { get; set; }
        public Station? CurrentStation { get; set; }
        public bool IsLanding { get; set; }
        public FlightState State { get; internal set; }

        public FlightDb DBFlight;

        public Flight(int id, bool isLanding, FlightState state) {
            this.Id = id;
            this.IsLanding = isLanding;
            this.State = state;

            DBFlight = new FlightDb { FlightId = id, State = state.ToString() };
        }

        public async Task Run(Route route) {
            CurrentStation = route.FirstOrDefault();
            await Task.Run(async () => {
                Station? nextStation;
                List<Station>? nextStations;
                do {
                    //$"flight {Id} is in station {CurrentStation?.Id}  now!".PrintColor(ConsoleColor.Red);
                    nextStations = route.GetNext(CurrentStation!);

                    if (nextStations.Count == 0) {
                        CurrentStation?.Exit();
                        CurrentStation = null;
                        return;
                    }

                    nextStation = await GetFirstAvilable(nextStations);

                } while (nextStation != null && nextStations.Count > 0);
            });
        }


        private async Task<Station?> GetFirstAvilable(List<Station> nextStations) {
            CancellationTokenSource cts = new();

            if (!nextStations.Any()) {
                return null;
            }

            IEnumerable<Task<Station>>? enterStationTasks = nextStations
             .Select(async x => {
                 return await x.Enter(this, cts);
             });

            Task<Station>? theOne = await Task.WhenAny(enterStationTasks);

            if (theOne != null) {
                await MoveToNextStation(await theOne);
            }

            return await theOne!;
        }

        private async Task MoveToNextStation(Station nextStation) {
            if (CurrentStation != null) {
                await CurrentStation.Exit();
            }

            CurrentStation = nextStation;

            //$"flight {Id} is moving to station {nextStation.Id}".PrintColor(ConsoleColor.Red);
            await Task.Delay(CurrentStation.WaitTime);
        }

    }
}