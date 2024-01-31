using HolidaySearch.DataAccess.Interfaces;
using HolidaySearch.DataContracts;
using Flight = HolidaySearch.Models.Flight;

namespace HolidaySearch.Application
{
    public class FlightService : IFlightService
    {
        private readonly IFlightRepository _flightRepository;

        public FlightService(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }
        public async Task<IEnumerable<Flight>?> SearchFlights(SearchRequest request)
        {
            Predicate<Flight>? flightsPredicate;

            if (request.DepartingFrom == null)
                flightsPredicate = f => f.DepartureDate == request.DepartureDate && f.To == request.TravelingTo;
            else
                flightsPredicate = f => f.DepartureDate == request.DepartureDate && f.From == request.DepartingFrom && f.To == request.TravelingTo;

            var searchResults = await _flightRepository.Search(flightsPredicate);
            return searchResults?.OrderBy(p => p.Price);
        }
    }
    
}
