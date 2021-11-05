using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FirstXamarinApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Firebase.Auth;

[assembly: Dependency(typeof(FirstXamarinApp.Droid.Dependencies.Auth))]
namespace FirstXamarinApp.Droid.Dependencies
{
    public class Auth : IAuth
    {
        public Auth()
        {
        }
        public async Task<bool> RegisterUser(string email, string password)
        {
            try
            {
                await FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, password);
                return true;
            }
            catch (FirebaseAuthWeakPasswordException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (FirebaseAuthInvalidCredentialsException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (FirebaseAuthUserCollisionException ex)
            {
                throw new Exception("There is no user record corresponding to this identifier");
            }
            catch (Exception ec)
            {
                throw new Exception("There is an unknkown error.");
            }
        }

        public async Task<bool> LoginUser(string email, string password)
        {
            try
            {
                await FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);
                return true;
            }
            catch (FirebaseAuthWeakPasswordException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (FirebaseAuthInvalidCredentialsException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (FirebaseAuthInvalidUserException ex)
            {
                throw new Exception("There is no user record corresponding to this identifier");
            }
            catch (Exception ec)
            {
                throw new Exception("There is an unknkown error.");
            }
        }

        public bool IsAuthenticated()
        {
            return FirebaseAuth.Instance.CurrentUser != null;
        }

        public string GetCurrentUserId()
        {
            return FirebaseAuth.Instance.CurrentUser.Uid;
        }
    }
}