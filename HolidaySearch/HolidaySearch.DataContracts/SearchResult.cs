namespace HolidaySearch.DataContracts
{
    public class SearchResult
    {
        public double TotalPrice { get; set; }
        public Flight Flight { get; set; }
        public Hotel Hotel { get; set; }
    }
}