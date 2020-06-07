using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PreFlight.AI.Shared;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PreFlight.Client.Droid
{
    public class Mapping
    {

        //construct the types at the server, send only what's needed to the client.

        public async Task StartFrom(Coordinates coordinates, string LocationName)
        {
            var location = new Location(coordinates);
            var options = new MapLaunchOptions { Name = locationName };

            await Map.OpenAsync(location, options);
        }

        public async Task NavigateTo(Coordinates coordinates, NavigationMode mode)
        {
            var location = new Location(coordinates);
            var options = new MapLaunchOptions { NavigationMode = mode };

            await Map.OpenAsync(location, options);
        }

        public async Task Placemark(Placemark placeMark, string locationName)
        {
            var placemark = new Placemark(placeMark);
            var options = new MapLaunchOptions { Name = locationName };

            await Map.OpenAsync(placemark, options);
        }
    }
}