namespace Hasslefreebooking.Models.Entities
{
    public class Traineschedules
    {
        public int ScheduleID { get; set; }
        public string TrainNumber { get; set; }
        public string DepartureStation { get; set; }
        public string ArrivalStation { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public int TotalSeats { get; set; }
        public int AvailableSeats { get; set; }
        public string Frequency { get; set; }

        // Navigation properties to represent the relationship with Stations
        public Stations DepartureStationNavigation { get; set; }
        public Stations ArrivalStationNavigation { get; set; }
    }
}
