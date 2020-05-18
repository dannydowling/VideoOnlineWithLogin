using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PreFlight.AI.Server.Services.HttpClients
{
    public class userHttpClient : HttpClient
    {
        private readonly HttpClient clientUser;

        public userHttpClient(HttpClient httpClient)
        {

            clientUser = httpClient;

            clientUser.BaseAddress = new Uri("http://localhost:44336");
            clientUser.Timeout = new TimeSpan(0, 0, 30);

            clientUser.DefaultRequestHeaders.Clear();
            clientUser.DefaultRequestHeaders.Accept.Add

                (new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}

