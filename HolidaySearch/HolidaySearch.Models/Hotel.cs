namespace HolidaySearch.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ArrivalDate { get; set; }
        public double PricePerNight { get; set; }
        public List<string> LocalAirports { get; set; }
        public int Nights { get; set; }
    }
}