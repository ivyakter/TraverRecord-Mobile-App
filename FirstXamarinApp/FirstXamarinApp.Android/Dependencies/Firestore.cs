using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Firestore;
using FirstXamarinApp.Helpers;
using FirstXamarinApp.Model;
using Java.Interop;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(FirstXamarinApp.Droid.Dependencies.Firestore))]
namespace FirstXamarinApp.Droid.Dependencies
{
    public class Firestore : Java.Lang.Object, IFirestore, IOnCompleteListener
    {
        List<Post> posts;
        bool hasReadPost = false;
        public Firestore()
        {
            posts = new List<Post>();
        }
       
        public async Task<bool> Delete(Post post)
        {
            try
            {
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("posts");
                collection.Document(post.Id).Delete();
                return true;
            }
            catch (Exception ex)
            {
                return false;

            }
        }
               
        public bool Insert(Post post)
        {
            try
            {
                var postDocument = new Dictionary<string, Java.Lang.Object>
                {
                    { "experience", post.Experience },
                    { "venuename", post.VenueName },
                    { "categoryId", post.CategoryId },
                    { "categoryName", post.CategoryName },
                    { "address", post.Address },
                    { "latitude", post.Latitude },
                    { "longitude", post.Longitude },
                    { "distance", post.Distance },
                    { "userId", Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid },
                };


                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("posts");
                collection.Add(new HashMap(postDocument));

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void OnComplete(Android.Gms.Tasks.Task task)
        {
            if (task.IsSuccessful)
            {
                var documents = (QuerySnapshot)task.Result;
                
                posts.Clear();
                foreach (var doc in documents.Documents)
                {
                    string? a = doc.Get("experience").ToString();
                    Post newPost = new Post()
                    {
                        Experience = doc.Get("experience").ToString(),
                        VenueName = doc.Get("venuename").ToString(),
                        CategoryId = doc.Get("categoryId").ToString(),
                        CategoryName = doc.Get("categoryName").ToString(),
                        Address = doc.Get("address").ToString(),
                        Latitude = (double)doc.Get("latitude"),
                        Longitude = (double)doc.Get("longitude"),
                        Distance = (int)doc.Get("distance"),
                        UserId = doc.Get("userId").ToString(),
                        Id = doc.Id

                       // Experience = "this is experience",
                       // VenueName = "venuename",
                       // CategoryId = "4bf58dd8d48988d162941735",
                       // Address = "1600 Amphitheatre Pkwy",
                       // Latitude = (double)37.42199675152714,
                       // Longitude = (double)-122.0840715663578,
                       // UserId = "9hggJmvmesZW5wyrBxrwxh0VSm32",
                       // Id = doc.Id
                    };

                    posts.Add(newPost);
                }
            }
            else
            {
                posts.Clear();
            }

            hasReadPost = true;
        }

        public async Task<List<Post>> Read()
        {
            try
            {
                hasReadPost = false;
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("posts");
                var query = collection.WhereEqualTo("userId", Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid);
                query.Get().AddOnCompleteListener(this);

               // for (int i = 0; i < 50; i++)
               // {
               //     await System.Threading.Tasks.Task.Delay(100);
               //     if (hasReadPost)
               //         break;
               // }

                return posts;
            }
            catch(Exception ex)
            {
                return posts;
            }

            return posts;
        }

        
        public async Task<bool> Update(Post post)
        {
            try
            {
                var postDocument = new Dictionary<string, Java.Lang.Object>
                {
                    { "experience", post.Experience },
                    { "venuename", post.VenueName },
                    { "categoryId", post.CategoryId },
                    { "categoryName", post.CategoryName },
                    { "address", post.Address },
                    { "latitude", post.Latitude },
                    { "longitude", post.Longitude },
                    { "distance", post.Distance },
                    { "userId", Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid },
                };

                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("posts");
                collection.Document(post.Id).Update(postDocument);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}