using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidaySearch.Application.Interfaces
{
    public interface IAirportLookupService
    {
        public Task<string> GetArrivalAirport(string travelingTo);
        public Task<List<string>?> GetDepartingAirport(string departingFrom);
    }
}
