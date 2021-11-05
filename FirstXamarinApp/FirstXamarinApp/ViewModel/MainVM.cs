using FirstXamarinApp.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace FirstXamarinApp.ViewModel
{
    public class MainVM : INotifyPropertyChanged
    {
        public Command LoginCommand { get; set; }
        private string email;
        public string Email { get { return email; } set { email = value; OnPropertyChanged("EntriesHasText"); } }
        private string password { get; set; }
        public string Password { get { return password; } set { password = value; OnPropertyChanged("EntriesHasText"); } }

        private bool entriesHasText;
        public bool EntriesHasText { get { return !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password); }  }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainVM()
        {
            LoginCommand = new Command<bool>(Login, CanLogin);
        }

        

        private async void Login(bool parameter)
        {
            //Authenticate
            bool result = await Auth.LoginUser(Email, Password);

            if (result)
                await App.Current.MainPage.Navigation.PushAsync(new HomePage());

        }

        private bool CanLogin(bool peremeter)
        {
           // if (!string.IsNullOrEmpty(Email) || !string.IsNullOrEmpty(Password))
           // {
           //     return true;
           // }
            return EntriesHasText;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
