using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foundation;
using PreFlightAI.Shared.Places;
using UIKit;
using Xamarin.Essentials;

namespace PreFlight.Client.iOS
{
    public class Mapping
    {

        public async Task StartFrom(Coordinates coordinates)
        {
            var location = new Xamarin.Essentials.Location(coordinates.Latitude, coordinates.Longitude);
            var options = new MapLaunchOptions { Name = "Start Location" };

            await Map.OpenAsync(location, options);
        }

        public async Task NavigateTo(Coordinates coordinates)
        {
            var location = new Xamarin.Essentials.Location(coordinates.Latitude, coordinates.Longitude);
            var options = new MapLaunchOptions { NavigationMode = NavigationMode.None };

            await Map.OpenAsync(location, options);
        }
    }
}