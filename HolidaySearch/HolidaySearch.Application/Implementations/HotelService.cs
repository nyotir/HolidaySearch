using HolidaySearch.DataAccess;
using HolidaySearch.DataContracts;
using Hotel = HolidaySearch.Models.Hotel;

namespace HolidaySearch.Application
{
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository _hotelRepository;
        public HotelService(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }
        public async Task<IEnumerable<Hotel>> SearchHotels(SearchRequest request)
        {
            Predicate<Hotel> hotelsPredicate = h => h.ArrivalDate == request.DepartureDate && h.LocalAirports.Contains(request.TravelingTo) && h.Nights >= request.Duration;
            var searchResults = await _hotelRepository.Search(hotelsPredicate);
            return searchResults.OrderBy(p => p.PricePerNight * request.Duration);
        }
    }
}
