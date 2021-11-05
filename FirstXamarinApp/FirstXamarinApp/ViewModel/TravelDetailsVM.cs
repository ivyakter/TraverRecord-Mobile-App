using FirstXamarinApp.Helpers;
using FirstXamarinApp.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FirstXamarinApp.ViewModel
{
    public class TravelDetailsVM
    {
        public Command UpdateCommand { get; set; }
        public Command DeleteCommand { get; set; }
        public Post SelectedPost { get; set; }
        public TravelDetailsVM()
        {
            UpdateCommand = new Command<string>(Update, CanUpdate);
            DeleteCommand = new Command(Delete);
        }

        private bool CanUpdate(string newExperience)
        {
            if (string.IsNullOrWhiteSpace(newExperience))
                return false;
            return true;
        }

        private async void Update(string newExperience)
        {
            SelectedPost.Experience = newExperience;
            var row = await Firestore.Update(SelectedPost);
            if (row)
                await App.Current.MainPage.Navigation.PopAsync();
        }
        private async void Delete()
        {
            var row = await Firestore.Delete(SelectedPost);
            if (row)
                await App.Current.MainPage.Navigation.PopAsync();
        }

    }
}
