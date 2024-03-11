using PlatformService.Dtos;
using System.Text;
using System.Text.Json;

namespace PlatformService.SyncDataServices.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        #region Private Constants
        private const string commandService = "CommandService";
        #endregion

        #region Private Fields
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        #endregion

        #region Constructor
        public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        #endregion

        #region ICommandDataClient Implementation
        public async Task SendPlatformToCommand(PlatformReadDto platformReadDto)
        {
            StringContent httpContent = new StringContent(
                JsonSerializer.Serialize(platformReadDto),
                Encoding.UTF8,
                "application/json");

            string commandPlatformUrl = _configuration[commandService];
            HttpResponseMessage response = await _httpClient.PostAsync(commandPlatformUrl, httpContent);

            if (response.IsSuccessStatusCode)
                Console.WriteLine($"--> Sync POST to Command Service was OK! {response.StatusCode} : {response.Content} : {response.ReasonPhrase}");
            else
                Console.WriteLine($"--> Sync POST to Command Service was NOT OK! {response.StatusCode} : {response.Content} : {response.ReasonPhrase}");
        }
        #endregion
    }
}
