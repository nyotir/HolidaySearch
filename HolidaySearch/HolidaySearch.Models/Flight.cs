namespace HolidaySearch.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public string Airline { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public double Price { get; set; }
        public DateTime DepartureDate { get; set; }
    }
}