using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace PreFlightAI.Server.Services
{
    public class Position
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        private string IP;
        private string locationurl;

        public Position() { }

        public Position(HttpClient httpClient, HttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient ?? throw new System.ArgumentNullException(nameof(httpClient));
            _httpContextAccessor = httpContextAccessor ?? throw new System.ArgumentNullException(nameof(httpContextAccessor));
        }

        public async Task IPLookup(CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://icanhazip.com");

            using (var response = await _httpClient.SendAsync(
                request, HttpCompletionOption.ResponseHeadersRead,
                cancellationToken))
            {
                response.EnsureSuccessStatusCode();
                IP = await response.Content.ReadAsStringAsync();
            }            
        }

        public async Task<string> LookupLocation(CancellationToken cancellationToken)
        {  
            await IPLookup(_cancellationTokenSource.Token);

            var request = new HttpRequestMessage(HttpMethod.Get, ( $"http://ip-api.com/json/" + IP));

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

        public string translateCity(string city, CancellationToken cancellationToken)
        {
            using (StreamReader sr = new StreamReader("Positions.json"))
            {
                JArray a = JArray.Parse(sr.ReadToEnd());

                var icaoJToken = a.First(x => String.Equals(x["city"].ToString(), city,
                                              StringComparison.InvariantCultureIgnoreCase)
                                              && !string.IsNullOrWhiteSpace(x["icao"].ToString()))["icao"];

                return String.Format(icaoJToken.ToString());
            }
        }
    }
}