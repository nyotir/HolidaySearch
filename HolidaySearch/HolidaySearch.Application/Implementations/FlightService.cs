using HolidaySearch.DataContracts;
using Flight = HolidaySearch.Models.Flight;

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
