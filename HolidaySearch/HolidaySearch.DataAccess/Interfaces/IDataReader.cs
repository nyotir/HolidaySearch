namespace HolidaySearch.DataAccess.Interfaces
{
    public interface IDataReader<T> where T : class
    {
        Task<List<T>?> Read(string resourceName);
    }
}
