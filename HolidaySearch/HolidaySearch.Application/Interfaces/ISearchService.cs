using HolidaySearch.DataContracts;

namespace HolidaySearch.Application.Interfaces
{
    public interface ISearchService
    {
        public Task<SearchResponse> SearchHoliday(SearchRequest request);
    }
}
