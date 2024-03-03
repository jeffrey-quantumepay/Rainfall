using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rainfall.Application.Queries
{
    public class GetReadingByStationIdCommandHandler : IRequestHandler<GetReadingByStationIdCommand, GetReadingByStationIdCommandResponse>
    {
        public GetReadingByStationIdCommandHandler()
        {

        }

        public async Task<GetReadingByStationIdCommandResponse> Handle(GetReadingByStationIdCommand request, CancellationToken cancellationToken)
        {

            // TO DO
            return new GetReadingByStationIdCommandResponse();
        }
    }
}
