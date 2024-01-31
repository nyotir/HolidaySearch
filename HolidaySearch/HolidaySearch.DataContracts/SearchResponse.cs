namespace HolidaySearch.DataContracts
{
    public class SearchResponse
    {
        public int TotalFlights { get; set; }
        public int TotalHotels { get; set; }
        public List<SearchResult> SearchResult { get; set; }
    }
}