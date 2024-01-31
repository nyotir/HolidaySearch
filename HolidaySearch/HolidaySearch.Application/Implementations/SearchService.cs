using HolidaySearch.DataContracts;

namespace HolidaySearch.Application
{
    public class SearchService : ISearchService
    {
        private readonly IHotelService _hotelService;
        private readonly IFlightService _flightService;

        public SearchService(IHotelService hotelService, IFlightService flightService)
        {
            _hotelService = hotelService;
            _flightService = flightService;
        }
        public async Task<SearchResponse> SearchHoliday(SearchRequest request)
        {
            var flights = await _flightService.SearchFlights(request);
            var hotels = await _hotelService.SearchHotels(request);

            var searchResults = (
                from flight in flights
                from hotel in hotels
                select new SearchResult
                {
                    Flight = new DataContracts.Flight
                    {
                        Id = flight.Id,
                        DepartingFrom = flight.From,
                        TravellingTo = flight.To,
                        Price = flight.Price
                    },

                    Hotel = new DataContracts.Hotel
                    {
                        Id = hotel.Id,
                        Name = hotel.Name,
                        Price = hotel.PricePerNight * request.Duration.Value
                    },

                    TotalPrice = flight.Price + hotel.PricePerNight * request.Duration.Value

                }).OrderBy(r => r.TotalPrice).ToList();

            return new SearchResponse { SearchResult = searchResults, TotalFlights = flights.Count(), TotalHotels = hotels.Count() };
        }
    }
}
