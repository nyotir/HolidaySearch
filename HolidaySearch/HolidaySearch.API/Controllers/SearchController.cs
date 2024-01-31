using HolidaySearch.Application;
using HolidaySearch.DataContracts;
using Microsoft.AspNetCore.Mvc;

namespace HolidaySearch.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase
    {

        private readonly ISearchService _searchService;
        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpPost(Name = "SearchHoliday")]
        public async Task<SearchResponse> Search(SearchRequest request)
        {
            return await _searchService.SearchHoliday(request);
        }
    }
}