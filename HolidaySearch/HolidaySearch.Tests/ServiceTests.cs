﻿using HolidaySearch.Application;
using HolidaySearch.DataContracts;
using Flight = HolidaySearch.Models.Flight;
using Hotel = HolidaySearch.Models.Hotel;
using Moq;
using Xunit;

namespace HolidaySearch.Tests
{
    public class ServiceTests
    {
        private readonly ISearchService _searchService;
        private readonly Mock<IFlightService> _flightService;
        private readonly Mock<IHotelService> _hotelService;

        public ServiceTests()
        {
            _flightService = new Mock<IFlightService>();
            _hotelService = new Mock<IHotelService>();
            _searchService = new SearchService(_hotelService.Object, _flightService.Object);
        }

        private void ValidSetup()
        {
            _flightService.Setup(f => f.SearchFlights(It.IsAny<SearchRequest>())).Returns(GetFlights);
            _hotelService.Setup(f => f.SearchHotels(It.IsAny<SearchRequest>())).Returns(GetHotels);
        }

        private Task<IEnumerable<Flight>> GetFlights()
        {
            var flightList = new List<Flight>
            {
                new () { Airline = "First Class Air", DepartureDate = DateTime.Parse("2023-07-01"), From = "MAN", To = "TFS", Id = 1, Price = 470.00 },
                new () { Airline = "Oceanic Airlines", DepartureDate = DateTime.Parse("2023-07-01"), From = "MAN", To = "AGP", Id = 2, Price = 245.00 },
                new () { Airline = "Trans American Airlines", DepartureDate = DateTime.Parse("2023-06-15"), From = "LGW", To = "PMI", Id = 3, Price = 170.00 },
                new () { Airline = "Trans American Airlines", DepartureDate = DateTime.Parse("2023-06-15"), From = "LTN", To = "PMI", Id = 4, Price = 153.00 },
                new () { Airline = "Fresh Airways", DepartureDate = DateTime.Parse("2023-07-01"), From = "LGW", To = "AGP", Id = 5, Price = 155 },
                new () { Airline = "Fresh Airways", DepartureDate = DateTime.Parse("2023-07-01"), From = "MAN", To = "AGP", Id = 6, Price = 140 },
                new () { Airline = "First Class Air", DepartureDate = DateTime.Parse("2023-07-01"), From = "MAN", To = "LPA", Id = 7, Price = 470.00 },
            };
            return Task.FromResult(flightList.AsEnumerable());
        }

        private Task<IEnumerable<Hotel>> GetHotels()
        {
            var hotelList = new List<Hotel>
            {
                new () { Id = 1, ArrivalDate = DateTime.Parse("2022-11-05"), LocalAirports = new List<string> { "TFS" }, Name = "Iberostar Grand Portals Nous", Nights = 7, PricePerNight = 100.00 },
                new () { Id = 2, ArrivalDate = DateTime.Parse("2022-11-05"), LocalAirports = new List<string> { "TFS" }, Name = "Laguna Park 2", Nights = 7, PricePerNight = 50 },
                new () { Id = 3, ArrivalDate = DateTime.Parse("2023-07-01"), LocalAirports = new List<string> { "AGP" }, Name = "Nh Malaga", Nights = 7, PricePerNight = 83.00 },
                new () { Id = 4, ArrivalDate = DateTime.Parse("2023-06-15"), LocalAirports = new List<string> { "PMI" }, Name = "Jumeirah Port Soller", Nights = 10, PricePerNight = 295.00 },
            };
            return Task.FromResult(hotelList.AsEnumerable());
        }


        [Fact]
        public async Task ValidInput_ReturnsCorrectResults()
        {
            ValidSetup();

            SearchRequest request = new()
            {
                DepartureDate = DateTime.Parse("2023-07-01"),
                DepartingFrom = "Manchester Airport (MAN)",
                Duration = 7,
                TravelingTo = "Malaga Airport (AGP)"
            };

            var response = await _searchService.SearchHoliday(request);

            Assert.Equal(28, response.SearchResult.Count);
            Assert.Equal(4, response.TotalHotels);
            Assert.Equal(7, response.TotalFlights);
        }

        [Fact]
        public async Task ValidInput_AlwaysReturnsBestPriceAsFirstResult()
        {
            ValidSetup();

            SearchRequest request = new()
            {
                DepartureDate = DateTime.Parse("2023-07-01"),
                DepartingFrom = "Manchester Airport (MAN)",
                Duration = 2,
                TravelingTo = "Malaga Airport (AGP)"
            };

            var response = await _searchService.SearchHoliday(request);

            Assert.Equal(240, response.SearchResult.FirstOrDefault().TotalPrice);
            Assert.Equal(253, response.SearchResult[1].TotalPrice);
            Assert.Equal(255, response.SearchResult[2].TotalPrice);
        }

       
        [Fact]
        public async Task NonSpecificDepartureAirport_ReturnsDataForAllDepartureAirports()
        {
            ValidSetup();

            SearchRequest request = new()
            {
                DepartureDate = DateTime.Parse("2023-06-15"),
                DepartingFrom = "Any London Airport",
                Duration = 7,
                TravelingTo = "Mallorca Airport (PMI)"
            };
            var response = await _searchService.SearchHoliday(request);
            Assert.Equal(4, response.TotalHotels);
            Assert.Equal(7, response.TotalFlights);
        }

      
    }
}