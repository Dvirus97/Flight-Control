using AirPort.BL;
using AirPort.DB.Models;
using AirPort.SignalR;
using AirPort3.DB.Repositories;
using AirPort3.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace AirPort.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AirPortController : ControllerBase {
        private readonly AirPortLogic logic;
        private readonly RouteProvider routeProvider;
        private readonly IRepository<FlightDb> repository;

        public AirPortController(AirPortLogic logic, RouteProvider routeProvider, IRepository<FlightDb> repository) {
            this.logic = logic;
            this.routeProvider = routeProvider;
            this.repository = repository;
        }




        [HttpGet("Landing/{flight}")]
        public IActionResult AddLanding(int flight) {
            Console.WriteLine($"new flight {flight}");
            var route = routeProvider.GetLanding;
            if (route.FlightCount > 2) {
                return BadRequest("rejected flight");
            }

            _ = logic.AddFlight(flight, route, FlightState.Landing); // fire and forget
            return Ok($"get new flight - {flight}");
        }

        [HttpGet("Departure/{flight}/")]
        public IActionResult AddDeparture(int flight) {
            Console.WriteLine($"new flight {flight}");
            var route = routeProvider.GetDeparture;
            if (route.FlightCount > 2) {
                return BadRequest("rejected flight");
            }

            _ = logic.AddFlight(flight, route, FlightState.Departure); // fire and forget
            return Ok($"get new flight - {flight}");
        }



        [HttpGet("GetFlightHistory/{count}")]
        public IEnumerable<FlightDb> GetFlightHistory(int count) {
            return repository.GetLast(count);
        }

        [HttpGet("CheckConnection")]
        public IActionResult ChechConnection() {
            return Ok("Main Server API Connected");
        }
    }
}
