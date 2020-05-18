using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PreFlight.AI.Server.Services.HttpClients
{
    public class weatherHttpClient : HttpClient
    {
        private readonly HttpClient clientWeather;

        public weatherHttpClient(HttpClient httpClient)
        {

            clientWeather = httpClient;

            clientWeather.BaseAddress = new Uri("http://localhost:46633/");

            clientWeather.Timeout = new TimeSpan(0, 0, 30);

            clientWeather.DefaultRequestHeaders.Clear();
            clientWeather.DefaultRequestHeaders.Accept.Add

                (new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}

