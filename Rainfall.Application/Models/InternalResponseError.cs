using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Rainfall.Application.Models
{
    public class InternalResponseError
    {

        public InternalResponseError(string stationdId)
        {
            StationId = stationdId;
        }

        [JsonPropertyName("stationId")]
        public string StationId { get; protected set; }
    }
}
