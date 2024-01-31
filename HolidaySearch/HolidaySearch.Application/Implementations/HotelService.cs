using HolidaySearch.Application.Interfaces;
using HolidaySearch.DataAccess.Interfaces;
using HolidaySearch.DataContracts;
using Hotel = HolidaySearch.Models.Hotel;

namespace HolidaySearch.Application.Implementations
{
    public class HotelService : IHotelService
    {
        private readonly IAirportLookupService _airportLookupService;
        private readonly IHotelRepository _hotelRepository;
        public HotelService(IAirportLookupService airportLookupService, IHotelRepository hotelRepository)
        {
            _airportLookupService = airportLookupService;
            _hotelRepository = hotelRepository;
        }
        public async Task<IEnumerable<Hotel>> SearchHotels(SearchRequest request)
        {
            var travelingTo = await _airportLookupService.GetArrivalAirport(request.TravelingTo);

            Predicate<Hotel> hotelsPredicate = h => h.ArrivalDate == request.DepartureDate && h.LocalAirports.Contains(travelingTo) && h.Nights >= request.Duration;
            var searchResults = await _hotelRepository.Search(hotelsPredicate);
            return searchResults.OrderBy(p => p.PricePerNight * request.Duration);
        }
    }
}
