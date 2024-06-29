namespace Hasslefreebooking
{
    public class FlightschedulesDTO
    {
        public int ScheduleID { get; set; }
        public int FlightID { get; set; } // Foreign key to associate schedule with flight
        public string Frequency { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
    }
}
