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
    public partial class ProfilePage : ContentPage
    {
        private ProfileVM vm;
        public ProfilePage()
        {
            InitializeComponent();
            vm = Resources["vm"] as ProfileVM;
        }

        protected override  void OnAppearing()
        {
            base.OnAppearing();

            vm.GetPost();

            // using (SQLiteConnection con = new SQLiteConnection(App.DatabaseLocation))
            // {
            //     var postTable = con.Table<Post>().ToList();

             //   var postTable = await Firestore.Read();

             // var categories = (from p in postTable
             //                   orderby p.CategoryId
             //                   select p.CategoryName).Distinct().ToList();
             //
             // var categories2 = postTable.OrderBy(p => p.CategoryId).Select(p=>p.CategoryName).Distinct().ToList();
             //
             // Dictionary<string, int> categoriesCount = new Dictionary<string, int>();
             //
             // foreach (var category in categories)
             // {
             //     var count = (from post in postTable
             //                  where post.CategoryName == category
             //                  select post).ToList().Count;
             //
             //     var count2 = postTable.Where(p => p.CategoryName == category).ToList().Count;
             //
             //     categoriesCount.Add(category, count);
             // }
             // categoriesListView.ItemsSource = categoriesCount;

             //TODO:   postCountLabel.Text = postTable.Count.ToString();
            //}
        }
    }
}