using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidaySearch.DataAccess.Interfaces
{
    public interface IAirportLookupRepository
    {
        public Task<List<string>?> Search(string key);
    }
}
