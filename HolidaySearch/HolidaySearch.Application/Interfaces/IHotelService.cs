using HolidaySearch.DataContracts;
using Hotel = HolidaySearch.Models.Hotel;

namespace HolidaySearch.Application.Interfaces
{
    public interface IHotelService
    {
        public Task<IEnumerable<Hotel>> SearchHotels(SearchRequest request);
    }
}
