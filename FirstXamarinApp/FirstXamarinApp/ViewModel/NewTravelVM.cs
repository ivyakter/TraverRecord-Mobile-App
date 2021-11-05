using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using FirstXamarinApp.Model;
using System.ComponentModel;
using Xamarin.Forms;
using System.Linq;
using FirstXamarinApp.Helpers;

namespace FirstXamarinApp.ViewModel
{
    public class NewTravelVM : INotifyPropertyChanged
    {
        public ObservableCollection<Venue> Venues { get; set; }

        public Command SaveCommand { get; set; }

        private string experience;
        public string Experience
        {
            get { return experience; }
            set { experience = value; OnPrepertyChanged("Experience"); OnPrepertyChanged("IsPostReady"); }
        }

        private bool isPostReady;
        public bool IsPostReady
        {
            get { return !string.IsNullOrEmpty(Experience) && SelectedVenue != null;  }
        }

        private Venue selectedVenue;
        public Venue SelectedVenue { get { return selectedVenue; } set { selectedVenue = value; OnPrepertyChanged("IsPostReady"); } }

        public event PropertyChangedEventHandler PropertyChanged;
        public NewTravelVM()
        {
            Venues = new ObservableCollection<Venue>();
            SaveCommand = new Command<bool>(Save, CanSave);
        }

        private void Save(bool parameter)
        {
            try
            {
                var firstCategory = selectedVenue.categories.FirstOrDefault();

                Post post = new Post()
                {
                    Experience = Experience,
                    CategoryId = firstCategory.id,
                    CategoryName = firstCategory.name,
                    Address = selectedVenue.location.address,
                    Distance = selectedVenue.location.distance,
                    Latitude = selectedVenue.location.lat,
                    Longitude = selectedVenue.location.lng,
                    VenueName = selectedVenue.name
                };

                bool row = Firestore.Insert(post);
                if (row)
                    App.Current.MainPage.DisplayAlert("Success", "Experience succesfully inserted", "ok");
                else
                    App.Current.MainPage.DisplayAlert("Failure", "Experience failed to be inserted", "ok");
            }
            catch (NullReferenceException nex)
            {

            }
            catch (Exception ex)
            {

            }
        }

        public bool CanSave(bool parameter)
        {
            return parameter;
        }

        public async void GetVenues(double lat, double lng)
        {
            var venues = await Venue.GetVenues(lat, lng);
            Venues.Clear();
            foreach (var venue in venues)
            {
                Venues.Add(venue);

            }
        }

        private void OnPrepertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
