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
    public partial class HistoryPage : ContentPage
    {
        private HistoryVM vm;
        public HistoryPage()
        {
            InitializeComponent();
            vm = Resources["vm"] as HistoryVM;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm.GetPost();

            // using (SQLiteConnection con = new SQLiteConnection(App.DatabaseLocation))
            // {
            //     con.CreateTable<Post>();
            //     var posts = con.Table<Post>().ToList();
            //     postListView.ItemsSource = posts;
            // }

            //postListView.ItemsSource = null;
            //var posts = await Firestore.Read();
            //postListView.ItemsSource = posts;
        }

        private void postListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedObject = postListView.SelectedItem as Post;
            if(selectedObject != null)
            {
                Navigation.PushAsync(new PostDetailsPage(selectedObject));
            }
        }
    }
}