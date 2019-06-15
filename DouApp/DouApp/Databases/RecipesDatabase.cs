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
        ObservableCollection<UserRecipe> mockRecipes;

        public RecipesDatabase()
        {
            PopulateMock();
        }

        void PopulateMock()
        {
            mockRecipes = new ObservableCollection<UserRecipe>();

            mockRecipes.Add(new UserRecipe()
            {
                UserID = 5,
                RecipeName = "FirstTest",
                LastUse = new DateTime(2019, 5, 30),
                Ingredient1 = "Cornflour",
                Amount1 = 700.0M,
                Type1 = "gr",
                Ingredient2 = "Flour",
                Amount2 = 560.0M,
                Type2 = "gr",
                Ingredient3 = "Poppyseed",
                Amount3 = 49.0M,
                Type3 = "gr",
                Ingredient4 = "Salt",
                Amount4 = 4.0M,
                Type4 = "tsp",
                Ingredient5 = "Yeast",
                Amount5 = 5.0M,
                Type5 = "tsp",
                Ingredient6 = "Soda Powder",
                Amount6 = 32.0M,
                Type6 = "tsp"
            });

            mockRecipes.Add(new UserRecipe()
            {
                UserID = 5,
                RecipeName = "SecondTest",
                LastUse = new DateTime(2019, 4, 27),
                Ingredient1 = "Cornflour",
                Amount1 = 700.0M,
                Type1 = "gr",
                Ingredient2 = "Flour",
                Amount2 = 560.0M,
                Type2 = "gr",
                Ingredient3 = "Poppyseed",
                Amount3 = 49.0M,
                Type3 = "gr",
                Ingredient4 = "Salt",
                Amount4 = 4.0M,
                Type4 = "tsp",
                Ingredient5 = "Yeast",
                Amount5 = 5.0M,
                Type5 = "tsp",
                Ingredient6 = "Soda Powder",
                Amount6 = 32.0M,
                Type6 = "tsp"
            });

            mockRecipes.Add(new UserRecipe()
            {
                UserID = 5,
                RecipeName = "ThirdTest",
                LastUse = new DateTime(2019, 6, 10),
                Ingredient1 = "Cornflour",
                Amount1 = 700.0M,
                Type1 = "gr",
                Ingredient2 = "Flour",
                Amount2 = 560.0M,
                Type2 = "gr",
                Ingredient3 = "Poppyseed",
                Amount3 = 1.0M,
                Type3 = "cups",
                Ingredient4 = "Salt",
                Amount4 = 3.0M,
                Type4 = "tsp",
                Ingredient5 = "Yeast",
                Amount5 = 2.0M,
                Type5 = "tsp",
                Ingredient6 = "Soda Powder",
                Amount6 = 1.0M,
                Type6 = "tbsp"
            });
        }

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

        public ObservableCollection<UserRecipe> GetRecipesMock()
        {
            return mockRecipes;
        }
    }
}
