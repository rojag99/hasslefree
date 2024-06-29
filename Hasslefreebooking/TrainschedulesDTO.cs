namespace Hasslefreebooking
{
    public class TrainschedulesDTO
    {

        public string TrainNumber { get; set; }
        public string DepartureStation { get; set; }
        public string ArrivalStation { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public int TotalSeats { get; set; }
        public int AvailableSeats { get; set; }
        public string Frequency { get; set; }
    }
}
