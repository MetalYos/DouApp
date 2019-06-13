using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        ObservableCollection<UserRecipe> recipes;

        public RecipesDatabase()
        {
            PopulateMock();
        }

        void PopulateMock()
        {
            recipes = new ObservableCollection<UserRecipe>();

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

            recipes.Add(new UserRecipe()
            {
                UserID = 5,
                RecipeName = "SecondTest",
                LastUse = new DateTime(2019, 4, 27),
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

            recipes.Add(new UserRecipe()
            {
                UserID = 5,
                RecipeName = "ThirdTest",
                LastUse = new DateTime(2019, 6, 10),
                Ingridient1 = "Cornflour",
                Amount1 = 700.0M,
                Type1 = "gr",
                Ingridient2 = "Flour",
                Amount2 = 560.0M,
                Type2 = "gr",
                Ingridient3 = "Poppyseed",
                Amount3 = 1.0M,
                Type3 = "cups",
                Ingridient4 = "Salt",
                Amount4 = 3.0M,
                Type4 = "tsp",
                Ingridient5 = "Yeast",
                Amount5 = 2.0M,
                Type5 = "tsp",
                Ingridient6 = "Soda Powder",
                Amount6 = 1.0M,
                Type6 = "tsp"
            });
        }

        public void AddRecipe(UserRecipe recipe)
        {
            UpdateIngredientsNames(recipe);
        }

        public void UpdateRecipe(UserRecipe recipe)
        {
            UpdateIngredientsNames(recipe);
        }

        public ObservableCollection<UserRecipe> GetRecipes(int userID)
        {
            return new ObservableCollection<UserRecipe>();
        }

        public ObservableCollection<UserRecipe> GetRecipesMock()
        {
            return recipes;
        }

        // Change any space to an underscore to match ingredients in database
        private void UpdateIngredientsNames(UserRecipe recipe)
        {
            recipe.Ingridient1.Replace(' ', '_');
            recipe.Ingridient2.Replace(' ', '_');
            recipe.Ingridient3.Replace(' ', '_');
            recipe.Ingridient4.Replace(' ', '_');
            recipe.Ingridient5.Replace(' ', '_');
            recipe.Ingridient6.Replace(' ', '_');
        }
    }
}
