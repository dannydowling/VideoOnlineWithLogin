using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace PreFlight.Logic.Location
{
    public class Position
    {
        public string GetIP(WebClient webClient) { return IPLookup(webClient); }
        private string IPLookup(WebClient client)
        {

            client.Headers.Add("Accept", "application / geo + json; version = 1");
            client.Headers.Add("User-Agent", "Density App");

            string IPwebsite = String.Format("https://icanhazip.com");
            string IP = client.DownloadString(IPwebsite);
            return IP.Replace("/n", "");
        }

        public string LookupLocation(WebClient webClient)
        {
            webClient.Headers.Add("Accept", "application / geo + json; version = 1");
            webClient.Headers.Add("User-Agent", "Density App");

            string IP = GetIP(webClient);
            var url = String.Format("http://ip-api.com/json/{0}", IP);
            var locationurl = webClient.DownloadString(url);

            JObject reader = JObject.Parse(locationurl);
            JToken CityAssert = reader.SelectToken("city");
            return CityAssert.ToString().Trim();
        }

        public string translateCity(string city)
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