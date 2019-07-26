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
        //decimal tspToGr = 4.26M;
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

        public List<string> GetIngredientsNamesByMeasuringType(string type)
        {
            List<string> ingsNames = new List<string>();
            var ingredients = GetIngredientsByMeasuringType(type);

            foreach (var ingredient in ingredients)
                ingsNames.Add(ingredient.ProductName);

            return ingsNames;
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

        public int GetIngredientIndexBySize(string ingredientName, bool isLarge)
        {
            int index = GetIngredientIndex(ingredientName);
            if (isLarge)
                return index;

            index -= GetIngredientsByMeasuringType("gr").Count;
            return index;
        }

        // Converts from cups, tsp and tbsp to gr
        public decimal ConvertToGr(string ingredientName, decimal amount, string type)
        {
            Ingredient ingredient = GetIngredient(ingredientName);
            if (ingredient == null)
                return amount;

            if (type == "gr")
                return amount;
            if (type == "cup")
                return amount * ingredient.Cup;
            if (type == "tsp")
                return amount * ingredient.Tsp;
            if (type == "tbsp")
                return amount * ingredient.Tbsp;

            return amount;
        }

        // Converts from tbsp to tsp
        public decimal ConvertToTsp(string ingredientName, decimal amount, string type)
        {
            Ingredient ingredient = GetIngredient(ingredientName);
            if (ingredient == null)
                return amount;

            if (type == "tsp")
                return amount; ;
            if (type == "tbsp")
                return amount * (ingredient.Tbsp / ingredient.Tsp);

            return amount;
        }

        public decimal ConvertToMl(string ingredientName, decimal amount, string type = "cup")
        {
            Ingredient ingredient = GetIngredient(ingredientName);
            if (ingredient == null)
                return amount;

            if (type == "ml")
                return amount; ;
            if (type == "cup")
                return amount * ingredient.Cup;

            return amount;
        }
    }
}
