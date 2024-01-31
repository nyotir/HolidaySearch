using HolidaySearch.DataContracts;

namespace HolidaySearch.Application
{
    public class FlightService : IFlightService
    {
        public async Task<IEnumerable<Flight>?> SearchFlights(SearchRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
