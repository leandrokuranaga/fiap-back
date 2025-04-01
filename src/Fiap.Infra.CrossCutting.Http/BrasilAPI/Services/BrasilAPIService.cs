
using Fiap.Infra.CrossCutting.Http.BrasilAPI.Models.Response;
using Fiap.Infra.Utils.Configurations;
using Microsoft.Extensions.Options;

namespace Fiap.Infra.CrossCutting.Http.ViaCEP.Services
{
    public class BrasilAPIService : BaseHttpService, IBrasilAPIService
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettings _appSettings;

        public BrasilAPIService(HttpClient httpClient,
        IOptionsSnapshot<AppSettings> appSettings
        ) : base(httpClient)
        {
            _appSettings = appSettings.Value;
            _httpClient = httpClient;
        }

        public Task<ApiResponse> GetState(int ddd)
        {
            throw new NotImplementedException();
        }
    }
}
