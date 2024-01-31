using HolidaySearch.DataAccess.Interfaces;
using HolidaySearch.Models;

namespace HolidaySearch.DataAccess.Implementations
{
    public class FlightRepository : IFlightRepository
    {
        private readonly IDataReader<Flight> _dataReader;
        public FlightRepository(IDataReader<Flight> dataReader)
        {
            _dataReader = dataReader;
        }
        public async Task<List<Flight>?> Search(Predicate<Flight> predicate)
        {
            var flights = await _dataReader.Read("Flights.json");

            return flights?.Where(f => predicate(f)).ToList();
        }
    }
}
