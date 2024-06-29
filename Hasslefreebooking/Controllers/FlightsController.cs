using Hasslefreebooking.Data;
using Hasslefreebooking.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hasslefreebooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly Hasslefreedbcontext hasslefreedbcontext;
        public FlightsController(Hasslefreedbcontext hasslefreedbcontext)
        {
            this.hasslefreedbcontext = hasslefreedbcontext;
        }
        // GET: api/flights
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FlightsDTO>>> GetFlights()
        {
            var flights = await hasslefreedbcontext.Flights
                .Select(f => new FlightsDTO
                {
                    FlightID = f.FlightID,
                    FlightNumber = f.FlightNumber,
                    Airline = f.Airline,
                    DepartureAirport = f.DepartureAirport,
                    ArrivalAirport = f.ArrivalAirport,
                    TotalSeats = f.TotalSeats,
                    AvailableSeats = f.AvailableSeats
                })
                .ToListAsync();

            return Ok(flights);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<FlightsDTO>> GetFlightById(int id)
        {
            // Find the flight with the given ID in the database
            var flight = await hasslefreedbcontext.Flights
                .FirstOrDefaultAsync(f => f.FlightID == id);

            // If flight is not found, return 404 Not Found
            if (flight == null)
            {
                return NotFound();
            }

            // Map the Flight entity to FlightsDTO
            var flightDTO = new FlightsDTO
            {
                FlightID = flight.FlightID,
                FlightNumber = flight.FlightNumber,
                Airline = flight.Airline,
                DepartureAirport = flight.DepartureAirport,
                ArrivalAirport = flight.ArrivalAirport,
                TotalSeats = flight.TotalSeats,
                AvailableSeats = flight.AvailableSeats
            };

            // Return the flight DTO with 200 OK status
            return Ok(flightDTO);
        }
        [HttpPost]
        public async Task<ActionResult<FlightsDTO>> PostFlight(FlightsDTO flightDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Map DTO to entity
            var flight = new Flights
            {
                FlightNumber = flightDto.FlightNumber,
                Airline = flightDto.Airline,
                DepartureAirport = flightDto.DepartureAirport,
                ArrivalAirport = flightDto.ArrivalAirport,
                TotalSeats = flightDto.TotalSeats,
                AvailableSeats = flightDto.AvailableSeats
            };

            // Add to database and save changes
            hasslefreedbcontext.Flights.Add(flight);
            await hasslefreedbcontext.SaveChangesAsync();

            // Return the created flight DTO with 201 Created status
            // You can optionally include a link to get the details of the newly created resource
            var flightDtoResponse = new FlightsDTO
            {
                FlightID = flight.FlightID,
                FlightNumber = flight.FlightNumber,
                Airline = flight.Airline,
                DepartureAirport = flight.DepartureAirport,
                ArrivalAirport = flight.ArrivalAirport,
                TotalSeats = flight.TotalSeats,
                AvailableSeats = flight.AvailableSeats
            };

            return CreatedAtAction(nameof(GetFlightById), new { id = flight.FlightID }, flightDtoResponse);
        }
        // PUT: api/flights/5
        // PUT: api/flights/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFlight(int id, PutFlightsDTO updateFlightDto)
        {
            

            var flight = await hasslefreedbcontext.Flights.FindAsync(id);

            if (flight == null)
            {
                return NotFound();
            }

            // Update flight entity with data from DTO
            flight.FlightNumber = updateFlightDto.FlightNumber;
            flight.Airline = updateFlightDto.Airline;
            flight.DepartureAirport = updateFlightDto.DepartureAirport;
            flight.ArrivalAirport = updateFlightDto.ArrivalAirport;
            flight.TotalSeats = updateFlightDto.TotalSeats;
            flight.AvailableSeats = updateFlightDto.AvailableSeats;

            try
            {
                await hasslefreedbcontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FlightExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlight(int id)
        {
            var flight = await hasslefreedbcontext.Flights.FindAsync(id);
            if (flight == null)
            {
                return NotFound();
            }

            hasslefreedbcontext.Flights.Remove(flight);
            await hasslefreedbcontext.SaveChangesAsync();

            return NoContent();
        }


        private bool FlightExists(int id)
        {
            return hasslefreedbcontext.Flights.Any(e => e.FlightID == id);
        }

    }
}
