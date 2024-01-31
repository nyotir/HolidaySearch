using HolidaySearch.Application.Interfaces;
using HolidaySearch.DataAccess.Interfaces;

namespace HolidaySearch.Application.Implementations
{
    public class AirportLookupService : IAirportLookupService
    {
        private readonly IAirportLookupRepository _airportLookupRepository;

        public AirportLookupService(IAirportLookupRepository airportLookupRepository)
        {
            _airportLookupRepository = airportLookupRepository;
        }
        public async Task<string> GetArrivalAirport(string travelingTo)
        {
            var arrivalAirports = await _airportLookupRepository.Search(travelingTo);
            if (arrivalAirports == null) throw new Exception("Arrival Airport could not be found.");
            if (arrivalAirports.Count > 1) throw new Exception("There cannot be more than one arrival airports.");
            return arrivalAirports.FirstOrDefault();
        }

        public async Task<List<string>?> GetDepartingAirport(string departingFrom)
        {
            return await _airportLookupRepository.Search(departingFrom);
        }
    }
}
