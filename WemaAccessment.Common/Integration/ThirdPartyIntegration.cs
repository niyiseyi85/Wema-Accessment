using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using WemaAccessment.Data.Dto;

namespace WemaAccessment.Common.Integration
{
  public class ThirdPartyIntegration : IThirdPartyIntegration
  {
    private readonly IConfiguration _configuration;
        public ThirdPartyIntegration(IConfiguration configuration)
        {
      _configuration = configuration;
    }
    
    public async Task<GetBankListResponseModel> GetBankList()
    {
      GetBankListResponseModel bankList;
      var actionUrl = $"{_configuration["WemaAPIBaseUrl"]}/api/Shared/GetAllBanks";
      try
      {
        var httpClientHandler = new System.Net.Http.HttpClientHandler()
        { Proxy = new WebProxy() { BypassProxyOnLocal = true } };
        ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13 | SecurityProtocolType.SystemDefault;
        httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true;
        httpClientHandler.DefaultProxyCredentials = CredentialCache.DefaultCredentials;
        var httpClient = new HttpClient(httpClientHandler, false);

        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var subscriptionKey = _configuration["WemaAPISubscriptionKey"];
        httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", $"{subscriptionKey}");

        var request = await httpClient.GetAsync(actionUrl).ConfigureAwait(false);
        var response = await request.Content.ReadAsStringAsync().ConfigureAwait(false);

        if (request.StatusCode == HttpStatusCode.OK)
        {
          bankList = JsonConvert.DeserializeObject<GetBankListResponseModel>(response);
        }
        else
        {
          bankList = new GetBankListResponseModel()
          {
            HasError = true,
            ErrorMessage = $"Unable to get a successful response from Client API. Statuscode: {request.StatusCode} - {response}"
          };
        }
      }
      catch (Exception ex)
      {
        bankList = new GetBankListResponseModel
        {
          HasError = true,
          ErrorMessage = $"Statuscode: 500 - {ex.Message} :: {ex.InnerException} :: {ex.StackTrace}"
        };
      }
      return bankList;
    }
  }
}
