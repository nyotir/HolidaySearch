using HolidaySearch.DataContracts;
using Hotel = HolidaySearch.Models.Hotel;

namespace HolidaySearch.Application
{
    public class HotelService : IHotelService
    {
        public async Task<IEnumerable<Hotel>> SearchHotels(SearchRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
