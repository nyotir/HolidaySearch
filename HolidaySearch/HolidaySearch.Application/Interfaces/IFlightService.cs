using HolidaySearch.DataContracts;

namespace HolidaySearch.Application
{
    public interface IFlightService
    {
        public Task<IEnumerable<Flight>?> SearchFlights(SearchRequest request);
    }
}
