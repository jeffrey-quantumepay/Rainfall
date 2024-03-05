using MediatR;
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
            var stationreading = new StationReading();
            try
            {
                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    using var httpResponseMessage = await httpClient.GetAsync($"{rainfallUrl}/id/stations/{request.StationId}/readings?_sorted&_limit={request.Count}");

                    var stream = await httpResponseMessage.Content.ReadAsStreamAsync();
                  

                    if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                    {
                        throw new RainfallInternalServerException("Internal Server Error");

                    }
                    else if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        throw new RainfallValidationException("Bad Request");
                    }
                    else if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        throw new RainfallNotFoundException("Not Found");
                    }
                    else
                    {
                        stationreading = stream.ReadAndDeserializeFromJson<StationReading>();

                        if (!stationreading.items.Any())
                        {
                            throw new RainfallNotFoundException("Not Found");
                        }
                    }

                    readings = _mapper.Map<List<StationReadingDto>>(stationreading.items);

                }
            }catch(Exception ex)
            {
                // to do: log error
                throw;
            }

            return new GetReadingByStationIdCommandResponse
            {
                readings = readings
            };
        }
    }
}
