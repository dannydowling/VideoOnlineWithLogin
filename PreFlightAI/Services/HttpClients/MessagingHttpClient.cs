using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PreFlight.AI.Server.Services.HttpClients
{
    public class messagingHttpClient : HttpClient
    {
        private readonly HttpClient clientMessaging;

        public messagingHttpClient(HttpClient httpClient)
        {

            clientMessaging = httpClient;

            clientMessaging.BaseAddress = new Uri("http://localhost:44336");
            clientMessaging.Timeout = new TimeSpan(0, 0, 30);

            clientMessaging.DefaultRequestHeaders.Clear();
            clientMessaging.DefaultRequestHeaders.Accept.Add

                (new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
