using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Rainfall.Application.Models
{
    public class StationReadingDto
    {
        [JsonPropertyName("dateMeasured")]
        public string measureDate { get; set; }

        [JsonPropertyName("amountMeasured")]
        public float amount { get; set; }
    }

}
