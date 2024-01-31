namespace HolidaySearch.DataContracts
{
    public class SearchRequest
    {
        public string DepartingFrom { get; set; }
        public string TravelingTo { get; set; }
        public DateTime? DepartureDate { get; set; }
        public int? Duration { get; set; }
    }
}
