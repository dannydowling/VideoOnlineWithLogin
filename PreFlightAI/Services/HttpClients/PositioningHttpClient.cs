using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PreFlight.AI.Server.Services.HttpClients
{
    public class positioningHttpClient : HttpClient
    {
        private readonly HttpClient clientLocation;

        public positioningHttpClient(HttpClient httpClient)
        {

            clientLocation = httpClient;

            clientLocation.BaseAddress = new Uri("http://localhost:44336");
            clientLocation.Timeout = new TimeSpan(0, 0, 30);

            clientLocation.DefaultRequestHeaders.Clear();
            clientLocation.DefaultRequestHeaders.Accept.Add

                (new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}

