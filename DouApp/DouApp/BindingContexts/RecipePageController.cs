using System;
using System.Collections.Generic;
using System.Text;

using DouApp.Models;

namespace DouApp.BindingContexts
{
    public class RecipeIngredientsList
    {
        public string ProductName { get; set; }
        public decimal Amount { get; set; }
        public int UnitsIndex { get; set; }
    }

    public class RecipePageController
    {
        public List<Container> Containers { get; set; }
        public List<Ingredient> LargeIngredients { get; set; }
        public List<Ingredient> SmallIngredients { get; set; }
        public UserRecipe Recipe { get; set; }
        public List<RecipeIngredientsList> Ingredients { get; set; }

        public RecipePageController(UserRecipe recipe)
        {
            LargeIngredients = App.Ingredients.GetIngredientsByType("gr");
            SmallIngredients = App.Ingredients.GetIngredientsByType("tsp");
            Containers = App.Containers.GetContainers();

            Recipe = recipe;
            PopulateIngredientsList();
        }

        private void PopulateIngredientsList()
        {
            Ingredients = new List<RecipeIngredientsList>();
            Ingredients.Add(new RecipeIngredientsList()
            {
                ProductName = Containers[0].Ingredient.ProductName,
                Amount = GetAmount(Containers[0].Ingredient.ProductName),
                UnitsIndex = GetUnitsIndex(Containers[0].Ingredient.Type)
            });
            Ingredients.Add(new RecipeIngredientsList()
            {
                ProductName = Containers[1].Ingredient.ProductName,
                Amount = GetAmount(Containers[1].Ingredient.ProductName),
                UnitsIndex = GetUnitsIndex(Containers[1].Ingredient.Type)
            });
            Ingredients.Add(new RecipeIngredientsList()
            {
                ProductName = Containers[2].Ingredient.ProductName,
                Amount = GetAmount(Containers[2].Ingredient.ProductName),
                UnitsIndex = GetUnitsIndex(Containers[2].Ingredient.Type)
            });
            Ingredients.Add(new RecipeIngredientsList()
            {
                ProductName = Containers[3].Ingredient.ProductName,
                Amount = GetAmount(Containers[3].Ingredient.ProductName),
                UnitsIndex = GetUnitsIndex(Containers[3].Ingredient.Type)
            });
            Ingredients.Add(new RecipeIngredientsList()
            {
                ProductName = Containers[4].Ingredient.ProductName,
                Amount = GetAmount(Containers[4].Ingredient.ProductName),
                UnitsIndex = GetUnitsIndex(Containers[4].Ingredient.Type)
            });
            Ingredients.Add(new RecipeIngredientsList()
            {
                ProductName = Containers[5].Ingredient.ProductName,
                Amount = GetAmount(Containers[5].Ingredient.ProductName),
                UnitsIndex = GetUnitsIndex(Containers[5].Ingredient.Type)
            });
        }

        private int GetUnitsIndex(string type)
        {
            if (type == "gr") return 0;
            else if (type == "cups") return 1;
            else return 2;
        }

        private decimal GetAmount(string containerIngredient)
        {
            if (containerIngredient == Recipe.Ingridient1)
                return Recipe.Amount1;
            if (containerIngredient == Recipe.Ingridient2)
                return Recipe.Amount2;
            if (containerIngredient == Recipe.Ingridient3)
                return Recipe.Amount3;
            if (containerIngredient == Recipe.Ingridient4)
                return Recipe.Amount4;
            if (containerIngredient == Recipe.Ingridient5)
                return Recipe.Amount5;
            if (containerIngredient == Recipe.Ingridient6)
                return Recipe.Amount6;

            return 0;
        }

        public void InitNewUserRecipe()
        {
            Recipe.UserID = App.UserID;
            Recipe.RecipeName = "";
            Recipe.LastUse = DateTime.Now;
            Recipe.Ingridient1 = Containers[0].Ingredient.ProductName;
            Recipe.Amount1 = 0;
            Recipe.Type1 = "gr";
            Recipe.Ingridient2 = Containers[1].Ingredient.ProductName;
            Recipe.Amount2 = 0;
            Recipe.Type2 = "gr";
            Recipe.Ingridient3 = Containers[2].Ingredient.ProductName;
            Recipe.Amount3 = 0;
            Recipe.Type3 = "gr";
            Recipe.Ingridient4 = Containers[3].Ingredient.ProductName;
            Recipe.Amount4 = 0;
            Recipe.Type4 = "tsp";
            Recipe.Ingridient5 = Containers[4].Ingredient.ProductName;
            Recipe.Amount5 = 0;
            Recipe.Type5 = "tsp";
            Recipe.Ingridient6 = Containers[5].Ingredient.ProductName;
            Recipe.Amount6 = 0;
            Recipe.Type6 = "tsp";

            PopulateIngredientsList();
        }
    }
}
