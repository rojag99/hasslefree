using System.ComponentModel.DataAnnotations;

namespace Hasslefreebooking.Models.Entities
{
    public class Flightschedules
    {
        [Key]
        public int ScheduleID { get; set; }
        public int FlightID { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public string Frequency { get; set; }

        // Navigation property to represent the relationship
        public Flights Flights { get; set; }
    }
}
