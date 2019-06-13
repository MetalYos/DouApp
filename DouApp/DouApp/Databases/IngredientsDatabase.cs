using System;
using System.Collections.Generic;
using System.Text;
using DouApp.Models;

namespace DouApp.Databases
{
    public class IngredientsDatabase
    {
        List<Ingredient> ingredients;

        public IngredientsDatabase()
        {
            ingredients = new List<Ingredient>();
            PopulateDatabase();
        }

        private void PopulateDatabase()
        {
            ingredients.Add(new Ingredient("Flour"));
            ingredients.Add(new Ingredient("Whole Flour"));
            ingredients.Add(new Ingredient("Cornflour"));
            ingredients.Add(new Ingredient("Cacao"));
            ingredients.Add(new Ingredient("Cream Wheat"));
            ingredients.Add(new Ingredient("White Sugar"));
            ingredients.Add(new Ingredient("Brown Sugar"));
            ingredients.Add(new Ingredient("Damrara Sugar"));
            ingredients.Add(new Ingredient("Powder Sugar"));
            ingredients.Add(new Ingredient("Almonds"));
            ingredients.Add(new Ingredient("Poppyseed"));
            ingredients.Add(new Ingredient("Nuts Powder"));
            ingredients.Add(new Ingredient("Coconut"));
            ingredients.Add(new Ingredient("Instant Pudding Vanila"));
            ingredients.Add(new Ingredient("Salt", "tsp"));
            ingredients.Add(new Ingredient("Baking Powder", "tsp"));
            ingredients.Add(new Ingredient("Soda Powder", "tsp"));
            ingredients.Add(new Ingredient("Yeast", "tsp"));
            ingredients.Add(new Ingredient("Gelatin Powder", "tsp"));
            ingredients.Add(new Ingredient("Cinnamon", "tsp"));
            ingredients.Add(new Ingredient("Nes Cafe", "tsp"));
        }

        public bool AddIngredient(Ingredient ingredient)
        {
            if (ContainsIngredient(ingredient))
                return false;

            ingredients.Add(ingredient);
            return true;
        }

        public bool RemoveIngredient(Ingredient ingredient)
        {
            if (!ContainsIngredient(ingredient))
                return false;

            ingredients.Remove(ingredient);
            return true;
        }

        public bool ContainsIngredient(Ingredient ingredient)
        {
            foreach (var ing in ingredients)
            {
                if (ing.ProductName == ingredient.ProductName)
                    return true;
            }

            return false;
        }

        public Ingredient GetIngredient(string name)
        {
            int index = GetIngredientIndex(name);

            if (index < 0)
                return null;

            return ingredients[index];
        }

        public Ingredient GetIngredient(int id)
        {
            return ingredients[id % ingredients.Count];
        }

        public List<Ingredient> GetIngredients()
        {
            return ingredients;
        }

        public List<Ingredient> GetIngredientsByType(string type)
        {
            List<Ingredient> ings = new List<Ingredient>();

            foreach (var ingredient in ingredients)
            {
                if (ingredient.Type == type)
                    ings.Add(ingredient);
            }

            return ings;
        }
        
        public int GetIngredientIndex(string ingredientName)
        {
            for (int i = 0; i < ingredients.Count; i++)
            {
                if (ingredients[i].ProductName.ToLower().Trim() == ingredientName.ToLower().Trim())
                    return i;
            }

            return -1;
        }

        public int GetIngredientInexBySize(string ingredientName, bool isLarge)
        {
            int index = GetIngredientIndex(ingredientName);
            if (isLarge)
                return index;

            index -= GetIngredientsByType("gr").Count;
            return index;
        }

        public decimal ConvertAmount(string ingredient, decimal amount, string type)
        {
            return 0;
        }
    }
}
