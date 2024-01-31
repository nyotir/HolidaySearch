using HolidaySearch.Application.Interfaces;
using HolidaySearch.DataAccess.Interfaces;
using HolidaySearch.DataContracts;
using Flight = HolidaySearch.Models.Flight;

namespace HolidaySearch.Application
{
    public class FlightService : IFlightService
    {
        private readonly IAirportLookupService _airportLookupService;
        private readonly IFlightRepository _flightRepository;

        public FlightService(IAirportLookupService airportLookupService, IFlightRepository flightRepository)
        {
            _airportLookupService = airportLookupService;
            _flightRepository = flightRepository;
        }
        public async Task<IEnumerable<Flight>?> SearchFlights(SearchRequest request)
        {
            var departingFrom = await _airportLookupService.GetDepartingAirport(request.DepartingFrom);
            var travelingTo = await _airportLookupService.GetArrivalAirport(request.TravelingTo);

            Predicate<Flight>? flightsPredicate;

            if (departingFrom == null)
                flightsPredicate = f => f.DepartureDate == request.DepartureDate && f.To == travelingTo;
            else
                flightsPredicate = f => f.DepartureDate == request.DepartureDate && departingFrom.Contains(f.From) && f.To == travelingTo;

            var searchResults = await _flightRepository.Search(flightsPredicate);
            return searchResults?.OrderBy(p => p.Price);
        }
    }
    
}
