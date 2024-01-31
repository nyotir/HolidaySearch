using Newtonsoft.Json;

namespace HolidaySearch.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonProperty("arrival_date")]
        public DateTime ArrivalDate { get; set; }

        [JsonProperty("price_per_night")]
        public double PricePerNight { get; set; }

        [JsonProperty("local_airports")]
        public List<string> LocalAirports { get; set; }
        public int Nights { get; set; }
    }
}