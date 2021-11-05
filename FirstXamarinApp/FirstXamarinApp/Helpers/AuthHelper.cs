﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace FirstXamarinApp.Helpers
{

    public interface IAuth
    {
        Task<bool> RegisterUser(string email, string password);
        Task<bool> LoginUser(string email, string password);
        bool IsAuthenticated();
        string GetCurrentUserId();
    }

    class Auth
    {
        private static IAuth auth = DependencyService.Get<IAuth>();
        public static async Task<bool> RegisterUser(string email, string password)
        {
            try
            {
                return await auth.RegisterUser(email, password);
            }
            catch(Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "ok");
                return false;
            }
        }

        public static async Task<bool> LoginUser(string email, string password)
        {
            try
            {
                return await auth.LoginUser(email, password);
            }
            catch(Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
                string registerMsg = "There is no user record corresponding to this identifier";
                    if (ex.Message.Contains(registerMsg))
                    return await RegisterUser(email, password);
                return false;
            }
        }

        public static bool IsAuthenticated()
        {
            return auth.IsAuthenticated();
        }

        public static string GetCurrentUserId()
        {
            return auth.GetCurrentUserId();
        }
    }

    
}
