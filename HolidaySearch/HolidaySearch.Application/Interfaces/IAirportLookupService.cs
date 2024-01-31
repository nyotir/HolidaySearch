namespace HolidaySearch.Application.Interfaces
{
    public interface IAirportLookupService
    {
        public Task<string> GetArrivalAirport(string travelingTo);
        public Task<List<string>?> GetDepartingAirport(string departingFrom);
    }
}
