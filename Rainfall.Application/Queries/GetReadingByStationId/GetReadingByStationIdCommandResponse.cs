using Rainfall.Application.Models;
using Rainfall.SharedLibrary.Responses;
using System.Collections.Generic;

namespace Rainfall.Application.Queries
{
    public class GetReadingByStationIdCommandResponse : BaseResponse
    {
        public GetReadingByStationIdCommandResponse() : base()
        {
            
        }

        public IList<StationReadingDto> readings { get; set; }
    }
}
