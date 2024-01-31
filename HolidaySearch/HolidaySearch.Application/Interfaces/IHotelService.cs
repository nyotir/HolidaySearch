using HolidaySearch.DataContracts;

namespace HolidaySearch.Application
{
    public interface IHotelService
    {
        public Task<IEnumerable<Hotel>> SearchHotels(SearchRequest request);
    }
}
