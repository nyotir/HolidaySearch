using HolidaySearch.DataContracts;
using System.Collections;

namespace HolidaySearch.Tests
{
    public class SearchTestData
    {
        public SearchRequest Request { get; set; }
        public SearchResponse Response { get; set; }
    }
    public class SearchRequestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new SearchTestData()
                {
                    Request = new()
                    {
                        DepartureDate = DateTime.Parse("2023-07-01"),
                        DepartingFrom = "Manchester Airport (MAN)",
                        Duration = 7,
                        TravelingTo = "Malaga Airport (AGP)"
                    },
                    Response = new()
                    {
                        TotalFlights = 2,
                        TotalHotels = 1,
                        SearchResult = new List<SearchResult>
                        {
                            new()
                            {
                                Flight = new Flight { DepartingFrom = "MAN", TravellingTo = "AGP", Id = 1, Price = 245.00 },
                                Hotel = new Hotel { Id = 1, Name = "Nh Malaga", Price = 83 },
                                TotalPrice = 826
                            },
                            new()
                            {
                                Flight = new Flight { DepartingFrom = "MAN", TravellingTo = "AGP", Id = 2, Price = 140 },
                                Hotel = new Hotel { Id = 1, Name = "Nh Malaga", Price = 83 },
                                TotalPrice = 721
                            }
                        }
                    }
                }
            };
            yield return new object[]
            {
                new SearchTestData()
                {
                    Request = new()
                    {
                        DepartureDate = DateTime.Parse("2023-06-15"),
                        DepartingFrom = "Any London Airport",
                        Duration = 10,
                        TravelingTo = "Mallorca Airport (PMI)"
                    },
                    Response = new()
                    {
                        TotalFlights = 2,
                        TotalHotels = 1,
                        SearchResult = new List<SearchResult>
                        {
                            new()
                            {
                                Flight = new Flight { DepartingFrom = "LGW", TravellingTo = "PMI", Id = 3, Price = 170.00 },
                                Hotel = new Hotel { Id = 1, Name = "Jumeirah Port Soller", Price = 295 },
                                TotalPrice = 3120
                            },
                            new()
                            {
                                Flight = new Flight { DepartingFrom = "LTN", TravellingTo = "PMI", Id = 4, Price = 153.00 },
                                Hotel = new Hotel { Id = 1, Name = "Jumeirah Port Soller", Price = 295 },
                                TotalPrice = 3103
                            }
                        }
                    }
                }
            };
            yield return new object[]
            {
                new SearchTestData()
                {
                    Request = new()
                    {
                        DepartureDate = DateTime.Parse("2023-07-01"),
                        DepartingFrom = "Any Airport",
                        Duration = 6,
                        TravelingTo = "Malaga Airport (AGP)"
                    },
                    Response = new()
                    {
                        TotalFlights = 3,
                        TotalHotels = 1,
                        SearchResult = new List<SearchResult>
                        {
                            new()
                            {
                                Flight = new Flight{ DepartingFrom = "Manchester Airport (MAN)", TravellingTo = "Malaga Airport (AGP)", Id = 2, Price = 245 },
                                Hotel = new Hotel { Id = 3, Name = "Nh Malaga", Price = 83 },
                                TotalPrice = 826
                            },
                            new()
                            {
                                Flight = new Flight { DepartingFrom = "Manchester Airport (LGW)", TravellingTo = "Malaga Airport (AGP)", Id = 5, Price = 155 },
                                Hotel = new Hotel { Id = 3, Name = "Nh Malaga", Price = 83 },
                                TotalPrice = 736
                            },
                            new()
                            {
                                Flight = new Flight { DepartingFrom = "Manchester Airport (MAN)", TravellingTo = "Malaga Airport (AGP)", Id = 6, Price = 140 },
                                Hotel = new Hotel { Id = 3, Name = "Nh Malaga", Price = 83 },
                                TotalPrice = 721
                            }
                        }
                    }
                }
            };
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
