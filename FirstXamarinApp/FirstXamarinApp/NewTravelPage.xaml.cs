using FirstXamarinApp.Helpers;
using FirstXamarinApp.Model;
using FirstXamarinApp.ViewModel;
using Plugin.Geolocator;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FirstXamarinApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewTravelPage : ContentPage
    {
        private NewTravelVM vm;
        public NewTravelPage()
        {
            InitializeComponent();
            vm = Resources["vm"] as NewTravelVM;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();

            vm.GetVenues(position.Latitude, position.Longitude);

           // var venues = await Venue.GetVenues(position.Latitude, position.Longitude);
           // venueListView.ItemsSource = venues;
        }
        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                var selecedVenue = venueListView.SelectedItem as Venue;
                var firstCategory = selecedVenue.categories.FirstOrDefault();

                Post post = new Post()
                {
                    Experience = experienceEntry.Text,
                    CategoryId = firstCategory.id,
                    CategoryName = firstCategory.name,
                    Address = selecedVenue.location.address,
                    Distance = selecedVenue.location.distance,
                    Latitude = selecedVenue.location.lat,
                    Longitude = selecedVenue.location.lng,
                    VenueName = selecedVenue.name
                };

                // using (SQLiteConnection con = new SQLiteConnection(App.DatabaseLocation))
                // {
                //     con.CreateTable<Post>();
                //     int row = con.Insert(post);
                //
                //     if (row > 0)
                //         DisplayAlert("Success", "Experience succesfully inserted", "ok");
                //     else
                //         DisplayAlert("Failure", "Experience failed to be inserted", "ok");
                // }

                bool row = Firestore.Insert(post);
                if (row)
                    DisplayAlert("Success", "Experience succesfully inserted", "ok");
                else
                    DisplayAlert("Failure", "Experience failed to be inserted", "ok");
            }
            catch (NullReferenceException nex)
            {

            }
            catch (Exception ex)
            {

            }

        }
    }
}