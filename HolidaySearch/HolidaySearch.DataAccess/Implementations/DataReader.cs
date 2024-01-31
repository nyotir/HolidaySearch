using HolidaySearch.DataAccess.Interfaces;
using Newtonsoft.Json;

namespace HolidaySearch.DataAccess.Implementations
{
    public class DataReader<T> : IDataReader<T> where T : class
    {
        public Task<List<T>?> Read(string resourceName)
        {
            var json = Extensions.Extensions.GetEmbeddedJson(resourceName);
            List<T>? entities = JsonConvert.DeserializeObject<List<T>>(json);

            return Task.FromResult(entities);
        }
    }
}
