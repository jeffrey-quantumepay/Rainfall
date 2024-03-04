using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Rainfall.SharedLibrary.Responses
{
    public class BaseResponse
    {
        [JsonIgnore]
        public bool Success { get; set; }

        [JsonIgnore]
        public string Message { get; set; }

        [JsonIgnore]
        public List<string> ValidationErrors { get; set; }

        public BaseResponse()
        {
            Success = true;
        }

        public BaseResponse(string message = null)
        {
            Success = true;
            Message = message;
        }

        public BaseResponse(string message, bool success)
        {
            Success = success;
            Message = message;
        }
    }
}
