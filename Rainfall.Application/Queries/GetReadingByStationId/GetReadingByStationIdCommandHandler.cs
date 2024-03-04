using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Rainfall.SharedLibrary.Responses;
using System.Text.Json;
using System;
using System.IO;
using System.Text;
using Rainfall.SharedLibrary.ExtensionMethods;
using Microsoft.Extensions.Configuration;
using Rainfall.Application.Models;
using AutoMapper;
using Rainfall.SharedLibrary.Exceptions;

namespace Rainfall.Application.Queries
{


    
    public class GetReadingByStationIdCommandHandler : IRequestHandler<GetReadingByStationIdCommand, GetReadingByStationIdCommandResponse>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public GetReadingByStationIdCommandHandler(
                IHttpClientFactory httpClientFactory,
                 IConfiguration config,
                   IMapper mapper)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
            _mapper = mapper;

        }

        public async Task<GetReadingByStationIdCommandResponse> Handle(GetReadingByStationIdCommand request, CancellationToken cancellationToken)
        {
            var rainfallUrl = _config.GetSection("MicroServiceConfig:RainfallEndpoint").Value;
            var readings = new List<StationReadingDto>();

            try
            {
                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    using var httpResponseMessage = await httpClient.GetAsync($"{rainfallUrl}/id/stations/{request.StationId}/readings?_sorted&_limit={request.Count}");

                    // To Do: check response

                    var stream = await httpResponseMessage.Content.ReadAsStreamAsync();
                    var result = stream.ReadAndDeserializeFromJson<StationReading>();

                    if (!result.items.Any())
                    {
                        throw new RainfallRecordNotFoundException();
                    }

                    readings = _mapper.Map<List<StationReadingDto>>(result.items);

                }
            }catch(Exception ex)
            {
                // to do: log error
            }

            return new GetReadingByStationIdCommandResponse
            {
                readings = readings
            };
        }
    }
}
