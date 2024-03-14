using AutoMapper;
using CommandService.Models;
using Grpc.Net.Client;
using PlatformService;

namespace CommandService.SyncDataServices.Grpc
{
    public class PlatformDataClient : IPlatformDataClient
    {
        #region Private Variables
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly string GrpcPlatform = "GrpcPlatform";
        #endregion

        #region Construction
        public PlatformDataClient(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }
        #endregion

        #region IPlatformDataClient Implementation
        public IEnumerable<Platform> ReturnAllPlatforms()
        {
            Console.WriteLine($"--> Calling GRPC Service {_configuration[GrpcPlatform]}");
            GrpcChannel channel = GrpcChannel.ForAddress(_configuration[GrpcPlatform]);
            GrpcPlatform.GrpcPlatformClient client = new GrpcPlatform.GrpcPlatformClient(channel);
            GetAllRequest request = new GetAllRequest();

            try
            {
                PlatformResponse reply = client.GetAllPlatforms(request);
                return _mapper.Map<IEnumerable<Platform>>(reply.Platform);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Couldn't call GRPC Server {ex.Message}");
                return null;
            }
        }
        #endregion
    }
}
