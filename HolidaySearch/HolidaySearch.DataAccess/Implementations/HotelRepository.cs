using HolidaySearch.DataAccess.Interfaces;
using HolidaySearch.Models;

namespace HolidaySearch.DataAccess.Implementations
{
    public class HotelRepository : IHotelRepository
    {
        private readonly IDataReader<Hotel> _dataReader;
        public HotelRepository(IDataReader<Hotel> dataReader)
        {
            _dataReader = dataReader;
        }
        public async Task<List<Hotel>?> Search(Predicate<Hotel> predicate)
        {
            var hotels = await _dataReader.Read("Hotels.json");

            return hotels?.Where(h => predicate(h)).ToList();
        }
    }
}
