using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using DouApp.Models;
using Newtonsoft.Json;

namespace DouApp.Databases
{
    public class IngredientsDatabase
    {
        string conversionTableUrl = @"https://dohconverter.azurewebsites.net/api/GetConvTable";
        List<Ingredient> ingredients;

        public IngredientsDatabase()
        {
            ingredients = new List<Ingredient>();
        }

        public void LoadTable()
        {
            User user = new User();

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(conversionTableUrl);
            httpWebRequest.Accept = "application/json; charset=utf-8";

            HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                ingredients = JsonConvert.DeserializeObject<List<Ingredient>>(result.ToString());
            }
        }

        /*
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
        */

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

        public List<Ingredient> GetIngredientsByMeasuringType(string type)
        {
            List<Ingredient> ings = new List<Ingredient>();

            foreach (var ingredient in ingredients)
            {
                if (ingredient.MeasuringType == type)
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

            index -= GetIngredientsByMeasuringType("gr").Count;
            return index;
        }

        public Ingredient GetIngredientConvert(string ingredientName)
        {
            foreach (var item in ingredients)
            {
                if (ingredientName.ToLower().Trim() == item.ProductName.ToLower().Trim())
                    return item;
            }
            return null;
        }

        public decimal ConvertToGr(string ingredientName, decimal amount, string type)
        {
            ingredientName = ingredientName.Replace(' ', '_');
            Ingredient ingredient = GetIngredientConvert(ingredientName);
            if (ingredient == null)
                return amount;

            if (type == "gr")
                return amount;
            if (type == "tsp")
                return amount * ingredient.Tsp;
            if (type == "tbsp")
                return amount * ingredient.Tbsp / ingredient.Tsp;
            if (type == "cups")
                return amount * ingredient.Cup;

            return amount;
        }
    }
}
