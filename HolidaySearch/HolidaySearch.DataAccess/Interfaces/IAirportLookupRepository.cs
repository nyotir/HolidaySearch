namespace HolidaySearch.DataAccess.Interfaces
{
    public interface IAirportLookupRepository
    {
        public Task<List<string>?> Search(string key);
    }
}
