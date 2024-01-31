using HolidaySearch.DataContracts;

namespace HolidaySearch.Application
{
    public interface ISearchService
    {
        public Task<SearchResponse> SearchHoliday(SearchRequest request);
    }
}
