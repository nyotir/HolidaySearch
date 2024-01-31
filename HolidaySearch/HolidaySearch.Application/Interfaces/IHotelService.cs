using HolidaySearch.DataContracts;
using Hotel = HolidaySearch.Models.Hotel;

namespace HolidaySearch.Application
{
    public interface IHotelService
    {
        public Task<IEnumerable<Hotel>> SearchHotels(SearchRequest request);
    }
}
