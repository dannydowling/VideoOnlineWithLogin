using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace PreFlight.Client.iOS
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