using HolidaySearch.Models;

namespace HolidaySearch.DataAccess.Interfaces
{
    public interface IHotelRepository
    {
        public Task<List<Hotel>?> Search(Predicate<Hotel> predicate);
    }
}
