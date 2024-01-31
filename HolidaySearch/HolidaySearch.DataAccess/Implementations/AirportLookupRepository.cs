using HolidaySearch.DataAccess.Interfaces;
using HolidaySearch.Models;

namespace HolidaySearch.DataAccess.Implementations
{
    public class AirportLookupRepository : IAirportLookupRepository
    {
        private readonly IDataReader<Airport> _dataReader;

        public AirportLookupRepository(IDataReader<Airport> dataReader)
        {
            _dataReader = dataReader;
        }
        public async Task<List<string>?> Search(string key)
        {
            var airports = await _dataReader.Read("Airports.json");

            var airport = airports?.FirstOrDefault(a => a.Tags.Contains(key));
            return airport?.Value.ToList();
        }
    }
}
