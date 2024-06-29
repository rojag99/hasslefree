using Hasslefreebooking.Data;
using Hasslefreebooking.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hasslefreebooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainschedulesController : ControllerBase

    {
        private readonly Hasslefreedbcontext hasslefreedbcontext;
        private readonly ILogger<TrainschedulesController> _logger; // Define ILogger
        public TrainschedulesController(Hasslefreedbcontext hasslefreedbcontext, ILogger<TrainschedulesController> logger)
        {
            this.hasslefreedbcontext = hasslefreedbcontext;
            _logger = logger;
        }
        // GET: api/TrainSchedules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Traineschedules>>> GetTrainSchedules()
        {
            var trainSchedules = await hasslefreedbcontext.Trainschedules
                .Select(ts => new Traineschedules
                {
                    ScheduleID = ts.ScheduleID,
                    TrainNumber = ts.TrainNumber,
                    DepartureStation = ts.DepartureStation,
                    ArrivalStation = ts.ArrivalStation,
                    DepartureTime = ts.DepartureTime,
                    ArrivalTime = ts.ArrivalTime,
                    TotalSeats = ts.TotalSeats,
                    AvailableSeats = ts.AvailableSeats,
                    Frequency = ts.Frequency
                })
                .ToListAsync();

            return trainSchedules;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TrainschedulesDTO>> GetTrainSchedule(int id)
        {
            var trainSchedule = await hasslefreedbcontext.Trainschedules
                .FirstOrDefaultAsync(ts => ts.ScheduleID == id);

            if (trainSchedule == null)
            {
                return NotFound();
            }

            // Map to DTO
            var trainScheduleDTO = new TrainschedulesDTO
            {
                
                TrainNumber = trainSchedule.TrainNumber,
                DepartureStation = trainSchedule.DepartureStation,
                ArrivalStation = trainSchedule.ArrivalStation,
                DepartureTime = trainSchedule.DepartureTime,
                ArrivalTime = trainSchedule.ArrivalTime,
                TotalSeats = trainSchedule.TotalSeats,
                AvailableSeats = trainSchedule.AvailableSeats,
                Frequency = trainSchedule.Frequency
            };

            return trainScheduleDTO;
        }
        [HttpPost]
        public async Task<ActionResult<TrainschedulesDTO>> PostTrainSchedule(TrainschedulesDTO trainScheduleDTO)
        {
            // Validate the incoming DTO
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return BadRequest with ModelState errors
            }

            try
            {
                // Map DTO to entity
                var trainScheduleEntity = new Traineschedules
                {
                    TrainNumber = trainScheduleDTO.TrainNumber,
                    DepartureStation = trainScheduleDTO.DepartureStation,
                    ArrivalStation = trainScheduleDTO.ArrivalStation,
                    DepartureTime = trainScheduleDTO.DepartureTime,
                    ArrivalTime = trainScheduleDTO.ArrivalTime,
                    TotalSeats = trainScheduleDTO.TotalSeats,
                    AvailableSeats = trainScheduleDTO.AvailableSeats,
                    Frequency = trainScheduleDTO.Frequency
                };

                // Add entity to DbContext and save changes
                hasslefreedbcontext.Trainschedules.Add(trainScheduleEntity);
                await hasslefreedbcontext.SaveChangesAsync();

                // Return created response with location header
                return CreatedAtAction(nameof(GetTrainSchedule), new { id = trainScheduleEntity.ScheduleID }, trainScheduleDTO);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                // Include detailed exception information for better diagnostics
                var errorMessage = $"Error creating train schedule: {ex.Message}. Stack Trace: {ex.StackTrace}. Inner Exception: {ex.InnerException?.Message}";

                // Log the error
                _logger.LogError(errorMessage);

                // Return 500 Internal Server Error with error message
                return StatusCode(500, "Internal server error occurred while saving the entity changes.");
            }
        }




    }
}
