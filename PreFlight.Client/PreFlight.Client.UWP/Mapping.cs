using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;


using Xamarin.Essentials;

namespace PreFlight.Client.UWP
{
    public class Mapping
    {

        public async Task StartFrom(Coordinates coordinates)
        {
            var location = new Location(coordinates);
            var options = new MapLaunchOptions { Name = "Start Location" };

            await Map.OpenAsync(location, options);
        }

        public async Task NavigateTo(Coordinates coordinates)
        {
            var location = new Location(coordinates);
            var options = new MapLaunchOptions { NavigationMode = NavigationMode.Direct };

            await Map.OpenAsync(location, options);
        }
    }
}