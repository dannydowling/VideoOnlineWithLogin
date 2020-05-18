using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PreFlight.AI.Server.Services.HttpClients
{

    public class employeeHttpClient : HttpClient
    {
        private readonly HttpClient clientEmployee;

        public employeeHttpClient(HttpClient httpClient)
        {

            clientEmployee = httpClient;

            clientEmployee.BaseAddress = new Uri("http://localhost:46633/");

            clientEmployee.Timeout = new TimeSpan(0, 0, 30);           
                       
            clientEmployee.DefaultRequestHeaders.Accept.Add                
                (new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
