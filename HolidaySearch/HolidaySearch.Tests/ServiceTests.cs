using HolidaySearch.Application;
using HolidaySearch.Application.Implementations;
using HolidaySearch.Application.Interfaces;
using HolidaySearch.DataAccess.Interfaces;
using HolidaySearch.DataContracts;
using Flight = HolidaySearch.Models.Flight;
using Hotel = HolidaySearch.Models.Hotel;
using Moq;
using Xunit;
using HolidaySearch.DataAccess;
using HolidaySearch.DataAccess.Implementations;
using HolidaySearch.Models;

namespace HolidaySearch.Tests
{
    public class ServiceTests
    {
        private readonly Mock<IDataReader<Flight>> _flightDataReader;
        private readonly Mock<IDataReader<Hotel>> _hotelDataReader;
        private readonly Mock<IDataReader<Airport>> _airportDataReader;

        private readonly IAirportLookupRepository _airportRepository;
        private readonly IFlightRepository _flightRepository;
        private readonly IHotelRepository _hotelRepository;

        private readonly IAirportLookupService _airportService;
        private readonly IFlightService _flightService;
        private readonly IHotelService _hotelService;

        private readonly ISearchService _searchService;

        public ServiceTests()
        {
            _airportDataReader = new Mock<IDataReader<Airport>>();
            _airportRepository = new AirportLookupRepository(_airportDataReader.Object);
            _airportService = new AirportLookupService(_airportRepository);

            _flightDataReader = new Mock<IDataReader<Flight>>();
            _flightRepository = new FlightRepository(_flightDataReader.Object);
            _flightService = new FlightService(_airportService, _flightRepository);

            _hotelDataReader = new Mock<IDataReader<Hotel>>();
            _hotelRepository = new HotelRepository(_hotelDataReader.Object);
            _hotelService = new HotelService(_airportService, _hotelRepository);

            _searchService = new SearchService(_hotelService, _flightService);
        }

        private void ValidSetup()
        {
            _airportDataReader.Setup(f => f.Read(It.IsAny<string>())).Returns(GetAirports);
            _flightDataReader.Setup(f => f.Read(It.IsAny<string>())).Returns(GetFlights);
            _hotelDataReader.Setup(f => f.Read(It.IsAny<string>())).Returns(GetHotels);
        }

        private Task<List<Airport>> GetAirports()
        {
            var airportList = new List<Airport>
            {
                new () { Key = "Manchester Airport (MAN)", Value = new List<string> { "MAN" }, Tags = new List<string>() {"Manchester Airport (MAN)", "MAN"}},
                new () { Key = "Any London Airport", Value = new List<string> { "LTN", "LGW" },Tags = new List<string>() { "Any London Airport", "LTN", "LGW" } },
                new () { Key = "Mallorca Airport (PMI)", Value = new List<string> { "PMI" },Tags = new List<string>() { "Mallorca Airport (PMI)", "PMI" } },
                new () { Key = "Malaga Airport (AGP)", Value = new List<string> { "AGP" },Tags = new List<string>() {"Malaga Airport (AGP)", "AGP" } },
                new () { Key = "Gran Canaria Airport (LPA)", Value = new List<string> { "LPA" },Tags = new List<string>() { "Gran Canaria Airport (LPA)", "LPA" } },
                new () { Key = "Tenerife South Airport (TFS)", Value = new List<string> { "TFS" }, Tags = new List<string>() { "Tenerife South Airport (TFS)", "TFS" } }
            };
            return Task.FromResult(airportList);
        }
        private Task<List<Flight>> GetFlights()
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
            return Task.FromResult(flightList);
        }

        private Task<List<Hotel>> GetHotels()
        {
            var hotelList = new List<Hotel>
            {
                new () { Id = 1, ArrivalDate = DateTime.Parse("2022-11-05"), LocalAirports = new List<string> { "TFS" }, Name = "Iberostar Grand Portals Nous", Nights = 7, PricePerNight = 100.00 },
                new () { Id = 2, ArrivalDate = DateTime.Parse("2022-11-05"), LocalAirports = new List<string> { "TFS" }, Name = "Laguna Park 2", Nights = 7, PricePerNight = 50 },
                new () { Id = 3, ArrivalDate = DateTime.Parse("2023-07-01"), LocalAirports = new List<string> { "AGP" }, Name = "Nh Malaga", Nights = 7, PricePerNight = 83.00 },
                new () { Id = 4, ArrivalDate = DateTime.Parse("2023-06-15"), LocalAirports = new List<string> { "PMI" }, Name = "Jumeirah Port Soller", Nights = 10, PricePerNight = 295.00 },
            };
            return Task.FromResult(hotelList);
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
                TravelingTo = "AGP"
            };

            var response = await _searchService.SearchHoliday(request);

            Assert.Equal(2, response.SearchResult.Count);
            Assert.Equal(1, response.TotalHotels);
            Assert.Equal(2, response.TotalFlights);
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
                TravelingTo = "AGP"
            };

            var response = await _searchService.SearchHoliday(request);

            Assert.Equal(306, response.SearchResult.FirstOrDefault().TotalPrice);
            Assert.Equal(411, response.SearchResult[1].TotalPrice);
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
                TravelingTo = "PMI"
            };
            var response = await _searchService.SearchHoliday(request);
            Assert.Equal(1, response.TotalHotels);
            Assert.Equal(2, response.TotalFlights);
        }

        [Fact]
        public async Task HotelDoesNotExist_ReturnsNoData()
        {
            ValidSetup();

            SearchRequest request = new()
            {
                DepartureDate = DateTime.Parse("2023-07-01"),
                DepartingFrom = "Manchester Airport (MAN)",
                Duration = 7,
                TravelingTo = "Gran Canaria Airport (LPA)"
            };
            var response = await _searchService.SearchHoliday(request);
            Assert.Equal(0, response.SearchResult.Count);
            Assert.Equal(0, response.TotalHotels);
            Assert.Equal(1, response.TotalFlights);
        }

        [Fact]
        public async Task FlightDoesNotExist_ReturnsNoData()
        {
            ValidSetup();

            SearchRequest request = new()
            {
                DepartureDate = DateTime.Parse("2023-01-01"),
                DepartingFrom = "Any London Airport",
                Duration = 7,
                TravelingTo = "Gran Canaria Airport (LPA)"
            };
            var response = await _searchService.SearchHoliday(request);
            Assert.Equal(0, response.TotalHotels);
            Assert.Equal(0, response.TotalFlights);
        }
    }
}