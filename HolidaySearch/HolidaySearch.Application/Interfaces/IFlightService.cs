using HolidaySearch.DataContracts;
using Flight = HolidaySearch.Models.Flight;

namespace HolidaySearch.Application
{
    public interface IFlightService
    {
        public Task<IEnumerable<Flight>?> SearchFlights(SearchRequest request);
    }
}
