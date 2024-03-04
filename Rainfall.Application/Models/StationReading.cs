using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rainfall.Application.Models
{
    public class StationReading
    {
        public IList<StationReadingItem> items { get; set; }
    }

    public class StationReadingItem
    {
        public string dateTime { get; set; }
        public float value { get; set; }
    }
}
