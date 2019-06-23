using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Diagnostics;

using DouApp.Models;
using Newtonsoft.Json;

namespace DouApp.Databases
{
    public class UserRecipeComparer : IComparer<UserRecipe>
    {
        public int Compare(UserRecipe x, UserRecipe y)
        {
            if (x.LastUse < y.LastUse)
                return -1;
            if (x.LastUse > y.LastUse)
                return 1;

            return 0;
        }
    }

    public class RecipesDatabase
    {
        string getRecipesUrl = @"https://dohconverter.azurewebsites.net/api/GetUserRecipes";
        string addOrUpdateUrl = @"https://dohconverter.azurewebsites.net/api/AddUserRecipe";

        public UserRecipe AddRecipe(UserRecipe recipe)
        {
            UserRecipe convertedRecipe = new UserRecipe();

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(addOrUpdateUrl);
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Method = "POST";
            httpWebRequest.Accept = "application/json; charset=utf-8";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string jsonOutput = JsonConvert.SerializeObject(recipe);

                streamWriter.Write(jsonOutput);
                streamWriter.Flush();
                streamWriter.Close();

                try
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        convertedRecipe = JsonConvert.DeserializeObject<UserRecipe>(result.ToString());
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }

            return convertedRecipe;
        }

        public UserRecipe UpdateRecipe(UserRecipe recipe)
        {
            return AddRecipe(recipe);
        }

        public ObservableCollection<UserRecipe> GetRecipes(int userID)
        {
            List<UserRecipe> userRecipes = new List<UserRecipe>();

            User user = new User
            {
                ID = userID,
                Username = "",
                Email = "",
                Password = ""
            };

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(getRecipesUrl);
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Method = "POST";
            httpWebRequest.Accept = "application/json; charset=utf-8";

            using (var reqStream = httpWebRequest.GetRequestStream())
            {
                using (var streamWriter = new StreamWriter(reqStream))
                {
                    string jsonOutput = JsonConvert.SerializeObject(user);

                    streamWriter.Write(jsonOutput);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    try
                    {
                        userRecipes = JsonConvert.DeserializeObject<List<UserRecipe>>(result.ToString());
                    }
                    catch (Exception e)
                    {
                        Debug.Write(e.Message);
                        userRecipes = new List<UserRecipe>();
                    }
                }
            }

            ObservableCollection<UserRecipe> recipes = new ObservableCollection<UserRecipe>(userRecipes);
            Helpers.SortUserRecipes(recipes, UserRecipe.CompareByLastUse);

            return recipes;
        }
    }
}
