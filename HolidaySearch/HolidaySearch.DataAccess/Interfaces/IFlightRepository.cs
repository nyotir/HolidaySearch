using HolidaySearch.Models;

namespace HolidaySearch.DataAccess.Interfaces
{
    public interface IFlightRepository
    {
        public Task<List<Flight>?> Search(Predicate<Flight> predicate);
    }
}
