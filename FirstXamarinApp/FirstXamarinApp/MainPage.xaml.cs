using FirstXamarinApp.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FirstXamarinApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            var assembly = typeof(MainPage);
            iconImage.Source = ImageSource.FromResource("FirstXamarinApp.Assets.Images.plane.png", assembly);
        }

        private async void loginButton_Clicked(object sender, EventArgs e)
        {
             if (!string.IsNullOrEmpty(emailEntry.Text) || !string.IsNullOrEmpty(passwordEntry.Text))
             {
                //Authenticate
               bool result =  await Auth.LoginUser(emailEntry.Text, passwordEntry.Text);

               if(result)
                 await Navigation.PushAsync(new HomePage());
             }
            //Navigation.PushAsync(new HomePage());
        }
    }
}
