using FirstXamarinApp.Helpers;
using FirstXamarinApp.Model;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using Position = Xamarin.Forms.Maps.Position;

namespace FirstXamarinApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            InitializeComponent();
        }
        IGeolocator locator = CrossGeolocator.Current;

        protected override void OnAppearing()
        {
            GetLocation();

            GetPosts();
        }

        private async void GetPosts()
        {
            // using (SQLiteConnection con = new SQLiteConnection(App.DatabaseLocation))
            // {
            //     con.CreateTable<Post>();
            //     var posts = con.Table<Post>().ToList();
            //
            //     DisplayOnMap(posts);
            // }

            var posts = await Firestore.Read();
            DisplayOnMap(posts);
        }

        private void DisplayOnMap(List<Post> posts)
        {
            foreach (var post in posts)
            {
                try
                {
                    var pinCoordinates = new Position(post.Latitude, post.Longitude);

                    var pin = new Pin()
                    {
                        Position = pinCoordinates,
                        Label = post.VenueName,
                        Address = post.Address,
                        Type = PinType.SavedPin
                    };

                    locationMap.Pins.Add(pin);
                }
                catch(Exception ex) { }
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            locator.StopListeningAsync();
        }
        private async void GetLocation()
        {
            var status = await CheckAndRequestLocationPermission();

            if(status == PermissionStatus.Granted)
            {
                var location = await Geolocation.GetLocationAsync();

                
                locator.PositionChanged += Locator_PositionChange;
                await locator.StartListeningAsync(new TimeSpan(0,1,0),100);

                locationMap.IsShowingUser = true;
                CenterMape(location.Latitude, location.Longitude);
            }
        }

        private void Locator_PositionChange(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            CenterMape(e.Position.Latitude, e.Position.Longitude);
        }

        private void CenterMape(double latitude, double longitude)
        {
            Xamarin.Forms.Maps.Position center = new Xamarin.Forms.Maps.Position(latitude, longitude);
            MapSpan span = new MapSpan(center, 1, 1);

            locationMap.MoveToRegion(span);
        }

        private async Task<PermissionStatus> CheckAndRequestLocationPermission()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

            if(status == PermissionStatus.Granted)
            return status;

            if(status == PermissionStatus.Denied && DeviceInfo.Platform == DevicePlatform.iOS)
            {
                //
                return status;
            }

            status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            return status;
        }
    }
}