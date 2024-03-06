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

        public InternalResponseError(string message, string stationdId)
        {
            Message = message;
            StationId = stationdId;
        }

        public InternalResponseError(string stationdId)
        {
            StationId = stationdId;
        }

        [JsonIgnore]
        public string StationId { get; protected set; }

        [JsonPropertyName("message")]
        public string Message { get; protected set; }
    }
}
