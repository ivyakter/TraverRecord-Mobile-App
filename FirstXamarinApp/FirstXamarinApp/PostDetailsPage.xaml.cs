using FirstXamarinApp.Helpers;
using FirstXamarinApp.Model;
using FirstXamarinApp.ViewModel;
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
    public partial class PostDetailsPage : ContentPage
    {
        Post selectedPost;
        public PostDetailsPage(Post selectedPost)
        {
            InitializeComponent();
            this.selectedPost = selectedPost;

            (Resources["vm"] as TravelDetailsVM).SelectedPost = selectedPost;
            experienceEntry.Text = selectedPost.Experience;
        }

        private async void updateButton_Clicked(object sender, EventArgs e)
        {
            selectedPost.Experience = experienceEntry.Text;
            // using (SQLiteConnection con = new SQLiteConnection(App.DatabaseLocation))
            // {
            //     con.CreateTable<Post>();
            //     int row = con.Update(selectedPost);
            //
            //     if (row > 0)
            //         DisplayAlert("Success", "Experience succesfully updated", "ok");
            //     else
            //         DisplayAlert("Failure", "Experience failed to be updated", "ok");
            // }

            var row = await Firestore.Update(selectedPost);
            if (row)
                await Navigation.PopAsync();
        }

        private async void deleteButton_Clicked(object sender, EventArgs e)
        {
            // using (SQLiteConnection con = new SQLiteConnection(App.DatabaseLocation))
            // {
            //     con.CreateTable<Post>();
            //     int row = con.Delete(selectedPost);
            //
            //     if (row > 0)
            //         DisplayAlert("Success", "Experience succesfully deleted", "ok");
            //     else
            //         DisplayAlert("Failure", "Experience failed to be deleted", "ok");
            // }

            var row = await Firestore.Delete(selectedPost);
            if (row)
                await Navigation.PopAsync();
        }
    }
}