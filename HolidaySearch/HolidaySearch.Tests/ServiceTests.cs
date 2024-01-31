using HolidaySearch.Application;
using HolidaySearch.DataContracts;
using Xunit;

namespace HolidaySearch.Tests
{
    public class ServiceTests
    {
        private readonly ISearchService _searchService;

        public ServiceTests()
        {
            _searchService = new SearchService();
        }

        [Fact]
        public async Task ValidInput_ReturnsCorrectResults()
        {
            SearchRequest request = new()
            {
                DepartureDate = DateTime.Parse("2023-07-01"),
                DepartingFrom = "Manchester Airport (MAN)",
                Duration = 7,
                TravelingTo = "Malaga Airport (AGP)"
            };

            var response = await _searchService.SearchHoliday(request);

            Assert.Equal(1, response.SearchResult.Count);
            Assert.Equal(1, response.TotalHotels);
            Assert.Equal(1, response.TotalFlights);
        }

        [Fact]
        public async Task ValidInput_AlwaysReturnsBestPriceAsFirstResult()
        {
            SearchRequest request = new()
            {
                DepartureDate = DateTime.Parse("2023-07-01"),
                DepartingFrom = "Manchester Airport (MAN)",
                Duration = 7,
                TravelingTo = "Malaga Airport (AGP)"
            };

            var response = await _searchService.SearchHoliday(request);

            Assert.Equal(1, response.SearchResult.FirstOrDefault().TotalPrice);
        }

       
        [Fact]
        public async Task NonSpecificDepartureAirport_ReturnsDataForAllDepartureAirports()
        {
            SearchRequest request = new()
            {
                DepartureDate = DateTime.Parse("2023-06-15"),
                DepartingFrom = "Any London Airport",
                Duration = 7,
                TravelingTo = "Mallorca Airport (PMI)"
            };
            var response = await _searchService.SearchHoliday(request);
            Assert.Equal(1, response.TotalHotels);
            Assert.Equal(1, response.TotalFlights);
        }

      
    }
}