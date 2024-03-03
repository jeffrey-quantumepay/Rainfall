using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rainfall.Application.Queries
{
    public class GetReadingByStationIdCommand : IRequest<GetReadingByStationIdCommandResponse>
    {
        public int Count { get; set; }
        public string? StationId { get; set; }
    }
}
