using System;
using System.Collections.Generic;
using System.Text;

using DouApp.Models;

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
        List<UserRecipe> recipes;

        public RecipesDatabase()
        {
            PopulateMock();
        }

        void PopulateMock()
        {
            recipes = new List<UserRecipe>();

            recipes.Add(new UserRecipe()
            {
                UserID = 5,
                RecipeName = "FirstTest",
                LastUse = new DateTime(2019, 5, 30),
                Ingridient1 = "Cornflour",
                Amount1 = 700.0M,
                Type1 = "gr",
                Ingridient2 = "Flour",
                Amount2 = 560.0M,
                Type2 = "gr",
                Ingridient3 = "Poppyseed",
                Amount3 = 49.0M,
                Type3 = "gr",
                Ingridient4 = "Salt",
                Amount4 = 4.0M,
                Type4 = "tsp",
                Ingridient5 = "Yeast",
                Amount5 = 5.0M,
                Type5 = "tsp",
                Ingridient6 = "Soda Powder",
                Amount6 = 32.0M,
                Type6 = "tsp"
            });
        }

        public void AddRecipe(UserRecipe recipe)
        {

        }

        public List<UserRecipe> GetRecipes(int userID)
        {
            return new List<UserRecipe>();
        }

        public List<UserRecipe> GetRecipesMock()
        {
            return recipes;
        }
    }
}
