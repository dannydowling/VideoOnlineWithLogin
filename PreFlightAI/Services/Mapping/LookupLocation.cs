using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PreFlight.AI.Server.Services.Mapping
{
    public class LookupLocation
    {

        private readonly HttpClient _httpClient;
        private readonly IPosition position;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        private string locationurl;
        private string IP;
        public LookupLocation()   {        }

        public LookupLocation(HttpClient httpClient, HttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient ?? throw new System.ArgumentNullException(nameof(httpClient));
            _httpContextAccessor = httpContextAccessor ?? throw new System.ArgumentNullException(nameof(httpContextAccessor));
        }

        public async Task<string> fetchLocation(CancellationToken cancellationToken)
        {
                        
                if (IP == null)
                {
                    await position.IPLookup(_cancellationTokenSource.Token);
                }

                var request = new HttpRequestMessage(HttpMethod.Get, ($"http://ip-api.com/json/" + IP));

                using (var response = await _httpClient.SendAsync(
                    request,
                    HttpCompletionOption.ResponseHeadersRead,
                    cancellationToken))
                {
                    response.EnsureSuccessStatusCode();
                    locationurl = await response.Content.ReadAsStringAsync();
                }               
           

            JObject reader = JObject.Parse(locationurl);
            JToken CityAssert = reader.SelectToken("city");
            return CityAssert.ToString().Trim();

        }
    }
}
