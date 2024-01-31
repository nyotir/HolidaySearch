using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HolidaySearch.Models;

namespace HolidaySearch.DataAccess
{
    public interface IHotelRepository
    {
        public Task<List<Hotel>?> Search(Predicate<Hotel> predicate);
    }
}
