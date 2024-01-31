using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidaySearch.Models
{
    public class Airport
    {
        public string Key { get; set; }
        public List<string> Tags { get; set; }
        public List<string> Value { get; set; }
    }
}
