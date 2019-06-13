using System;
using System.Collections.Generic;
using System.Text;

using DouApp.Models;
using DouApp.Interfaces;
using Xamarin.Forms;
using System.Linq;

namespace DouApp.BindingContexts
{
    // Represents a recipe in the list view in the RecipePage
    public class RecipeIngredientsList
    {
        public string ProductName { get; set; }
        public decimal Amount { get; set; }
        public int UnitsIndex { get; set; }
        public bool IsLarge { get; set; }
    }

    public class IngredientAmountToFill
    {
        public string ProductName { get; set; }
        public decimal AmountToFill { get; set; }
    }

    public class RecipePageController
    {
        // User recipe that will be used to check amounts against containers
        UserRecipe convertedRecipe;
        // User recipe that will be used to build command string
        UserRecipe commandRecipe;

        #region Properties
        // Is the recipe new or one from history
        public bool IsNew { get; set; }
        public List<Container> Containers { get; set; }
        public UserRecipe Recipe { get; set; }
        public List<RecipeIngredientsList> Ingredients { get; set; }
        #endregion

        public RecipePageController(UserRecipe recipe, bool isNew = false)
        {
            Recipe = recipe;
            IsNew = isNew;

            Containers = App.Containers.GetContainers();

            if (IsNew)
                InitNewUserRecipe();

            PopulateIngredientsList();
        }

        private void PopulateIngredientsList()
        {
            Ingredients = new List<RecipeIngredientsList>();
            Ingredients.Add(new RecipeIngredientsList()
            {
                ProductName = Containers[0].Ingredient,
                Amount = GetAmount(Containers[0].Ingredient),
                UnitsIndex = GetUnitsIndex(Recipe.Type1),
                IsLarge = Containers[0].IsLarge
            });
            Ingredients.Add(new RecipeIngredientsList()
            {
                ProductName = Containers[1].Ingredient,
                Amount = GetAmount(Containers[1].Ingredient),
                UnitsIndex = GetUnitsIndex(Recipe.Type2),
                IsLarge = Containers[1].IsLarge
            });
            Ingredients.Add(new RecipeIngredientsList()
            {
                ProductName = Containers[2].Ingredient,
                Amount = GetAmount(Containers[2].Ingredient),
                UnitsIndex = GetUnitsIndex(Recipe.Type3),
                IsLarge = Containers[2].IsLarge
            });
            Ingredients.Add(new RecipeIngredientsList()
            {
                ProductName = Containers[3].Ingredient,
                Amount = GetAmount(Containers[3].Ingredient),
                UnitsIndex = GetUnitsIndex(Recipe.Type4),
                IsLarge = Containers[3].IsLarge
            });
            Ingredients.Add(new RecipeIngredientsList()
            {
                ProductName = Containers[4].Ingredient,
                Amount = GetAmount(Containers[4].Ingredient),
                UnitsIndex = GetUnitsIndex(Recipe.Type5),
                IsLarge = Containers[4].IsLarge
            });
            Ingredients.Add(new RecipeIngredientsList()
            {
                ProductName = Containers[5].Ingredient,
                Amount = GetAmount(Containers[5].Ingredient),
                UnitsIndex = GetUnitsIndex(Recipe.Type6),
                IsLarge = Containers[5].IsLarge
            });
        }

        // Gets the unit index (for the units picker) according to ingredient type
        private int GetUnitsIndex(string type)
        {
            if (type == "gr") return 0;
            else if (type == "cups") return 1;
            else return 2;
        }

        // Gets the unit from it's index (from the units picker)
        private string GetUnitFromIndex(int index)
        {
            if (index == 0) return "gr";
            else if (index == 1) return "cups";
            else return "tsp";
        }

        // Gets the ingredient amount of the recipe according to the ingredient in the container
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

        // Update the amount and unit of each ingredient in the recipe,
        // according to what the user entered
        private void UpdateAmountAndUnits()
        {
            foreach (var ingredient in Ingredients)
            {
                if (ingredient.ProductName == Recipe.Ingridient1)
                {
                    Recipe.Amount1 = ingredient.Amount;
                    Recipe.Type1 = GetUnitFromIndex(ingredient.UnitsIndex);
                }
                if (ingredient.ProductName == Recipe.Ingridient1)
                {
                    Recipe.Amount2 = ingredient.Amount;
                    Recipe.Type2 = GetUnitFromIndex(ingredient.UnitsIndex);
                }
                if (ingredient.ProductName == Recipe.Ingridient1)
                {
                    Recipe.Amount3 = ingredient.Amount;
                    Recipe.Type3 = GetUnitFromIndex(ingredient.UnitsIndex);
                }
                if (ingredient.ProductName == Recipe.Ingridient1)
                {
                    Recipe.Amount4 = ingredient.Amount;
                    Recipe.Type4 = GetUnitFromIndex(ingredient.UnitsIndex);
                }
                if (ingredient.ProductName == Recipe.Ingridient1)
                {
                    Recipe.Amount5 = ingredient.Amount;
                    Recipe.Type5 = GetUnitFromIndex(ingredient.UnitsIndex);
                }
                if (ingredient.ProductName == Recipe.Ingridient1)
                {
                    Recipe.Amount6 = ingredient.Amount;
                    Recipe.Type6 = GetUnitFromIndex(ingredient.UnitsIndex);
                }
            }
        }

        // If the user pressed on new recipe, this method initializes the new recipe
        // with the current configuration
        public void InitNewUserRecipe()
        {
            Recipe.UserID = App.UserID;
            Recipe.RecipeName = "";
            Recipe.LastUse = DateTime.Now;
            Recipe.Ingridient1 = Containers[0].Ingredient;
            Recipe.Amount1 = 0;
            Recipe.Type1 = "gr";
            Recipe.Ingridient2 = Containers[1].Ingredient;
            Recipe.Amount2 = 0;
            Recipe.Type2 = "gr";
            Recipe.Ingridient3 = Containers[2].Ingredient;
            Recipe.Amount3 = 0;
            Recipe.Type3 = "gr";
            Recipe.Ingridient4 = Containers[3].Ingredient;
            Recipe.Amount4 = 0;
            Recipe.Type4 = "tsp";
            Recipe.Ingridient5 = Containers[4].Ingredient;
            Recipe.Amount5 = 0;
            Recipe.Type5 = "tsp";
            Recipe.Ingridient6 = Containers[5].Ingredient;
            Recipe.Amount6 = 0;
            Recipe.Type6 = "tsp";
        }

        public List<IngredientAmountToFill> CheckIfPossible()
        {
            CreateConvertedRecipe();
            List<IngredientAmountToFill> ingredients = new List<IngredientAmountToFill>();

            // Check if it is possible (if requested amounts are smaller than amounts in containers)
            // Each container which needs filling is added to the returned list
            foreach (var container in Containers)
            {
                if (container.Ingredient == convertedRecipe.Ingridient1)
                {
                    if ((container.Amount - convertedRecipe.Amount1) < 0)
                    {
                        ingredients.Add(new IngredientAmountToFill()
                        {
                            ProductName = convertedRecipe.Ingridient1,
                            AmountToFill = convertedRecipe.Amount1 - container.Amount
                        });
                        continue;
                    }
                }
                if (container.Ingredient == convertedRecipe.Ingridient2)
                {
                    if ((container.Amount - convertedRecipe.Amount2) < 0)
                    {
                        ingredients.Add(new IngredientAmountToFill()
                        {
                            ProductName = convertedRecipe.Ingridient2,
                            AmountToFill = convertedRecipe.Amount2 - container.Amount
                        });
                        continue;
                    }
                }
                if (container.Ingredient == convertedRecipe.Ingridient3)
                {
                    if ((container.Amount - convertedRecipe.Amount3) < 0)
                    {
                        ingredients.Add(new IngredientAmountToFill()
                        {
                            ProductName = convertedRecipe.Ingridient3,
                            AmountToFill = convertedRecipe.Amount3 - container.Amount
                        });
                        continue;
                    }
                }
                if (container.Ingredient == convertedRecipe.Ingridient4)
                {
                    if ((container.Amount - convertedRecipe.Amount4) < 0)
                    {
                        ingredients.Add(new IngredientAmountToFill()
                        {
                            ProductName = convertedRecipe.Ingridient4,
                            AmountToFill = convertedRecipe.Amount4 - container.Amount
                        });
                        continue;
                    }
                }
                if (container.Ingredient == convertedRecipe.Ingridient5)
                {
                    if ((container.Amount - convertedRecipe.Amount5) < 0)
                    {
                        ingredients.Add(new IngredientAmountToFill()
                        {
                            ProductName = convertedRecipe.Ingridient5,
                            AmountToFill = convertedRecipe.Amount5 - container.Amount
                        });
                        continue;
                    }
                }
                if (container.Ingredient == convertedRecipe.Ingridient6)
                {
                    if ((container.Amount - convertedRecipe.Amount6) < 0)
                    {
                        ingredients.Add(new IngredientAmountToFill()
                        {
                            ProductName = convertedRecipe.Ingridient6,
                            AmountToFill = convertedRecipe.Amount6 - container.Amount
                        });
                        continue;
                    }
                }
            }

            return ingredients;
        }

        public bool LetsDoh()
        {
            SaveRecipe();

            CreateCommandRecipe();

            // Start execution - create command string and send it via bluetooth
            string command = CreateCommandString();
            byte[] buffer = Encoding.ASCII.GetBytes(command.ToArray());

            if (DependencyService.Get<IBluetoothHelper>().IsConnected())
                DependencyService.Get<IBluetoothHelper>().WriteBufferToDevice(buffer);
            else
                return false;

            // Need to wait for confirmation at the end of the execution (???)

            // At the end, update the amounts in the containers
            UpdateAmountsInContainers();

            return true;
        }

        public void SaveRecipe()
        {
            // Update amounts and units in the recipe
            UpdateAmountAndUnits();

            Recipe.LastUse = DateTime.Now;

            if (IsNew)
                App.RecipesDB.AddRecipe(Recipe);
            else
                App.RecipesDB.UpdateRecipe(Recipe);
        }

        private void CreateConvertedRecipe()
        {
            convertedRecipe = new UserRecipe();
            convertedRecipe.UserID = Recipe.UserID;
            convertedRecipe.LastUse = Recipe.LastUse;

            convertedRecipe.Ingridient1 = Recipe.Ingridient1;
            convertedRecipe.Amount1 = Recipe.Amount1;
            if (Recipe.Type1 != "gr")
                convertedRecipe.Amount1 = App.Ingredients.ConvertAmount(Recipe.Ingridient1, Recipe.Amount1, Recipe.Type1);
            convertedRecipe.Type1 = "gr";

            convertedRecipe.Ingridient2 = Recipe.Ingridient2;
            convertedRecipe.Amount2 = Recipe.Amount2;
            if (Recipe.Type2 != "gr")
                convertedRecipe.Amount2 = App.Ingredients.ConvertAmount(Recipe.Ingridient2, Recipe.Amount2, Recipe.Type2);
            convertedRecipe.Type2 = "gr";

            convertedRecipe.Ingridient3 = Recipe.Ingridient3;
            convertedRecipe.Amount3 = Recipe.Amount3;
            if (Recipe.Type3 != "gr")
                convertedRecipe.Amount3 = App.Ingredients.ConvertAmount(Recipe.Ingridient3, Recipe.Amount3, Recipe.Type3);
            convertedRecipe.Type3 = "gr";

            convertedRecipe.Ingridient4 = Recipe.Ingridient4;
            convertedRecipe.Amount4 = Recipe.Amount4;
            if (Recipe.Type4 != "gr")
                convertedRecipe.Amount4 = App.Ingredients.ConvertAmount(Recipe.Ingridient4, Recipe.Amount4, Recipe.Type4);
            convertedRecipe.Type4 = "gr";

            convertedRecipe.Ingridient5 = Recipe.Ingridient5;
            convertedRecipe.Amount5 = Recipe.Amount5;
            if (Recipe.Type5 != "gr")
                convertedRecipe.Amount5 = App.Ingredients.ConvertAmount(Recipe.Ingridient5, Recipe.Amount5, Recipe.Type5);
            convertedRecipe.Type5 = "gr";

            convertedRecipe.Ingridient6 = Recipe.Ingridient6;
            convertedRecipe.Amount6 = Recipe.Amount6;
            if (Recipe.Type6 != "gr")
                convertedRecipe.Amount6 = App.Ingredients.ConvertAmount(Recipe.Ingridient6, Recipe.Amount6, Recipe.Type6);
            convertedRecipe.Type6 = "gr";
        }

        private void CreateCommandRecipe()
        {
            commandRecipe = new UserRecipe();
            commandRecipe.UserID = Recipe.UserID;
            commandRecipe.LastUse = Recipe.LastUse;

            commandRecipe.Ingridient1 = Recipe.Ingridient1;
            commandRecipe.Amount1 = Recipe.Amount1;
            if (Recipe.Type1 != "gr")
                commandRecipe.Amount1 = App.Ingredients.ConvertAmount(Recipe.Ingridient1, Recipe.Amount1, Recipe.Type1);
            commandRecipe.Type1 = "gr";

            commandRecipe.Ingridient2 = Recipe.Ingridient2;
            commandRecipe.Amount2 = Recipe.Amount2;
            if (Recipe.Type2 != "gr")
                commandRecipe.Amount2 = App.Ingredients.ConvertAmount(Recipe.Ingridient2, Recipe.Amount2, Recipe.Type2);
            commandRecipe.Type2 = "gr";

            commandRecipe.Ingridient3 = Recipe.Ingridient3;
            commandRecipe.Amount3 = Recipe.Amount3;
            if (Recipe.Type3 != "gr")
                commandRecipe.Amount3 = App.Ingredients.ConvertAmount(Recipe.Ingridient3, Recipe.Amount3, Recipe.Type3);
            commandRecipe.Type3 = "gr";

            commandRecipe.Ingridient4 = Recipe.Ingridient4;
            commandRecipe.Amount4 = Recipe.Amount4;
            commandRecipe.Type4 = Recipe.Type4;

            commandRecipe.Ingridient5 = Recipe.Ingridient5;
            commandRecipe.Amount5 = Recipe.Amount5;
            commandRecipe.Type5 = Recipe.Type5;

            commandRecipe.Ingridient6 = Recipe.Ingridient6;
            commandRecipe.Amount6 = Recipe.Amount6;
            commandRecipe.Type6 = Recipe.Type6;
        }

        private string CreateCommandString()
        {
            string command = "";

            command += "f1$" + commandRecipe.Amount1.ToString().PadLeft(3, '0') + ";";
            command += "f2$" + commandRecipe.Amount2.ToString().PadLeft(3, '0') + ";";
            command += "f3$" + commandRecipe.Amount3.ToString().PadLeft(3, '0') + ";";
            command += "f4$" + commandRecipe.Amount4.ToString().PadLeft(3, '0') + ";";
            command += "f5$" + commandRecipe.Amount5.ToString().PadLeft(3, '0') + ";";
            command += "f6$" + commandRecipe.Amount6.ToString().PadLeft(3, '0') + ";";

            return command;
        }

        private void UpdateAmountsInContainers()
        {
            App.Containers.RemoveFromContainer(1, convertedRecipe.Amount1);
            App.Containers.RemoveFromContainer(2, convertedRecipe.Amount2);
            App.Containers.RemoveFromContainer(3, convertedRecipe.Amount3);
            App.Containers.RemoveFromContainer(4, convertedRecipe.Amount4);
            App.Containers.RemoveFromContainer(5, convertedRecipe.Amount5);
            App.Containers.RemoveFromContainer(6, convertedRecipe.Amount6);
        }
    }
}
