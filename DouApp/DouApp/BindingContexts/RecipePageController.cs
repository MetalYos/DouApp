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
        public bool IsLiquid { get; set; }
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
        public bool IsPossible { get; set; }
        public List<Container> Containers { get; set; }
        public UserRecipe Recipe { get; set; }
        public List<RecipeIngredientsList> Ingredients { get; set; }
        #endregion

        public RecipePageController(UserRecipe recipe, bool isNew = false, bool isPossible = true)
        {
            Containers = App.Containers.GetContainers();
            Recipe = UpdateRecipeOrderToContainers(recipe);
            IsNew = isNew;
            IsPossible = isPossible;

            if (IsNew)
                InitNewUserRecipe();

            PopulateIngredientsList();
        }

        // The ingredients order in the recipe can be different than the order of the containers.
        // This method makes sure that the Ingredients order in the recipe will be the same order as the containers.
        private UserRecipe UpdateRecipeOrderToContainers(UserRecipe recipe)
        {
            UserRecipe newRecipe = new UserRecipe();
            newRecipe.RecipeName = recipe.RecipeName;
            newRecipe.UserID = recipe.UserID;
            newRecipe.LastUse = recipe.LastUse;

            newRecipe.Ingredient1 = Containers[0].Ingredient;
            newRecipe.Amount1 = GetIngredientAmountFromContainer(newRecipe.Ingredient1, recipe);
            newRecipe.Type1 = GetIngredientTypeFromContainer(newRecipe.Ingredient1, recipe);

            newRecipe.Ingredient2 = Containers[1].Ingredient;
            newRecipe.Amount2 = GetIngredientAmountFromContainer(newRecipe.Ingredient2, recipe);
            newRecipe.Type2 = GetIngredientTypeFromContainer(newRecipe.Ingredient2, recipe);

            newRecipe.Ingredient3 = Containers[2].Ingredient;
            newRecipe.Amount3 = GetIngredientAmountFromContainer(newRecipe.Ingredient3, recipe);
            newRecipe.Type3 = GetIngredientTypeFromContainer(newRecipe.Ingredient3, recipe);

            newRecipe.Ingredient4 = Containers[3].Ingredient;
            newRecipe.Amount4 = GetIngredientAmountFromContainer(newRecipe.Ingredient4, recipe);
            newRecipe.Type4 = GetIngredientTypeFromContainer(newRecipe.Ingredient4, recipe);

            newRecipe.Ingredient5 = Containers[4].Ingredient;
            newRecipe.Amount5 = GetIngredientAmountFromContainer(newRecipe.Ingredient5, recipe);
            newRecipe.Type5 = GetIngredientTypeFromContainer(newRecipe.Ingredient5, recipe);

            newRecipe.Ingredient6 = Containers[5].Ingredient;
            newRecipe.Amount6 = GetIngredientAmountFromContainer(newRecipe.Ingredient6, recipe);
            newRecipe.Type6 = GetIngredientTypeFromContainer(newRecipe.Ingredient6, recipe);

            newRecipe.Ingredient7 = Containers[6].Ingredient;
            newRecipe.Amount7 = GetIngredientAmountFromContainer(newRecipe.Ingredient7, recipe);

            newRecipe.Ingredient8 = Containers[7].Ingredient;
            newRecipe.Amount8 = GetIngredientAmountFromContainer(newRecipe.Ingredient8, recipe);

            return newRecipe;
        }

        // Gets an ingredient in a container and returns it's amount according to the given user recipe
        private decimal GetIngredientAmountFromContainer(string containerIngredient, UserRecipe userRecipe)
        {
            if (containerIngredient == userRecipe.Ingredient1)
                return userRecipe.Amount1;
            if (containerIngredient == userRecipe.Ingredient2)
                return userRecipe.Amount2;
            if (containerIngredient == userRecipe.Ingredient3)
                return userRecipe.Amount3;
            if (containerIngredient == userRecipe.Ingredient4)
                return userRecipe.Amount4;
            if (containerIngredient == userRecipe.Ingredient5)
                return userRecipe.Amount5;
            if (containerIngredient == userRecipe.Ingredient6)
                return userRecipe.Amount6;
            if (containerIngredient == userRecipe.Ingredient7)
                return userRecipe.Amount7;
            if (containerIngredient == userRecipe.Ingredient8)
                return userRecipe.Amount8;

            return 0;
        }

        // Gets an ingredient in a container and returns it's type according to the given user recipe
        private string GetIngredientTypeFromContainer(string containerIngredient, UserRecipe userRecipe)
        {
            if (containerIngredient == userRecipe.Ingredient1)
                return userRecipe.Type1;
            if (containerIngredient == userRecipe.Ingredient2)
                return userRecipe.Type2;
            if (containerIngredient == userRecipe.Ingredient3)
                return userRecipe.Type3;
            if (containerIngredient == userRecipe.Ingredient4)
                return userRecipe.Type4;
            if (containerIngredient == userRecipe.Ingredient5)
                return userRecipe.Type5;
            if (containerIngredient == userRecipe.Ingredient6)
                return userRecipe.Type6;
            if (containerIngredient == userRecipe.Ingredient7)
                return "cup";
            if (containerIngredient == userRecipe.Ingredient8)
                return "cup";

            return "gr";
        }

        // Populates the Ingredients list according to the ordered recipe
        private void PopulateIngredientsList()
        {
            Ingredients = new List<RecipeIngredientsList>();
            Ingredients.Add(new RecipeIngredientsList()
            {
                ProductName = Recipe.Ingredient1,
                Amount = Recipe.Amount1,
                UnitsIndex = GetUnitsIndex(Recipe.Type1),
                IsLarge = Containers[0].IsLarge,
                IsLiquid = false
            });
            Ingredients.Add(new RecipeIngredientsList()
            {
                ProductName = Recipe.Ingredient2,
                Amount = Recipe.Amount2,
                UnitsIndex = GetUnitsIndex(Recipe.Type2),
                IsLarge = Containers[1].IsLarge,
                IsLiquid = false
            });
            Ingredients.Add(new RecipeIngredientsList()
            {
                ProductName = Recipe.Ingredient3,
                Amount = Recipe.Amount3,
                UnitsIndex = GetUnitsIndex(Recipe.Type3),
                IsLarge = Containers[2].IsLarge,
                IsLiquid = false
            });
            Ingredients.Add(new RecipeIngredientsList()
            {
                ProductName = Recipe.Ingredient4,
                Amount = Recipe.Amount4,
                UnitsIndex = GetUnitsIndex(Recipe.Type4),
                IsLarge = Containers[3].IsLarge,
                IsLiquid = false
            });
            Ingredients.Add(new RecipeIngredientsList()
            {
                ProductName = Recipe.Ingredient5,
                Amount = Recipe.Amount5,
                UnitsIndex = GetUnitsIndex(Recipe.Type5),
                IsLarge = Containers[4].IsLarge,
                IsLiquid = false
            });
            Ingredients.Add(new RecipeIngredientsList()
            {
                ProductName = Recipe.Ingredient6,
                Amount = Recipe.Amount6,
                UnitsIndex = GetUnitsIndex(Recipe.Type6),
                IsLarge = Containers[5].IsLarge,
                IsLiquid = false
            });
            Ingredients.Add(new RecipeIngredientsList()
            {
                ProductName = Recipe.Ingredient7,
                Amount = Recipe.Amount7,
                UnitsIndex = 0,
                IsLarge = true,
                IsLiquid = true
            });
            Ingredients.Add(new RecipeIngredientsList()
            {
                ProductName = Recipe.Ingredient8,
                Amount = Recipe.Amount8,
                UnitsIndex = 0,
                IsLarge = true,
                IsLiquid = true
            });
        }

        // Gets the unit index (for the units picker) according to ingredient type
        private int GetUnitsIndex(string type)
        {
            if (type == "gr" || type == "tsp") return 0;
            if (type == "cup" || type == "tbsp") return 1;
            else return 2;
        }

        // Gets the unit from it's index (from the units picker)
        private string GetUnitFromIndex(int index, bool isLarge)
        {
            if (isLarge)
            {
                if (index == 0) return "gr";
                else return "cup";
            }
            else
            {
                if (index == 0) return "tsp";
                else return "tbsp";
            }
        }

        // Update the amount and unit of each ingredient in the recipe,
        // according to what the user entered
        private void UpdateAmountAndUnits()
        {
            Recipe.Amount1 = Ingredients[0].Amount;
            Recipe.Type1 = GetUnitFromIndex(Ingredients[0].UnitsIndex, Ingredients[0].IsLarge);

            Recipe.Amount2 = Ingredients[1].Amount;
            Recipe.Type2 = GetUnitFromIndex(Ingredients[1].UnitsIndex, Ingredients[1].IsLarge);

            Recipe.Amount3 = Ingredients[2].Amount;
            Recipe.Type3 = GetUnitFromIndex(Ingredients[2].UnitsIndex, Ingredients[2].IsLarge);

            Recipe.Amount4 = Ingredients[3].Amount;
            Recipe.Type4 = GetUnitFromIndex(Ingredients[3].UnitsIndex, Ingredients[3].IsLarge);

            Recipe.Amount5 = Ingredients[4].Amount;
            Recipe.Type5 = GetUnitFromIndex(Ingredients[4].UnitsIndex, Ingredients[4].IsLarge);

            Recipe.Amount6 = Ingredients[5].Amount;
            Recipe.Type6 = GetUnitFromIndex(Ingredients[5].UnitsIndex, Ingredients[5].IsLarge);

            Recipe.Amount7 = Ingredients[6].Amount;

            Recipe.Amount8 = Ingredients[7].Amount;
        }

        // If the user pressed on new recipe, this method initializes the new recipe
        // with the current configuration
        public void InitNewUserRecipe()
        {
            Recipe.UserID = App.UserID;
            Recipe.RecipeName = "";
            Recipe.LastUse = DateTime.Now;
            Recipe.Ingredient1 = Containers[0].Ingredient;
            Recipe.Amount1 = 0;
            Recipe.Type1 = "gr";
            Recipe.Ingredient2 = Containers[1].Ingredient;
            Recipe.Amount2 = 0;
            Recipe.Type2 = "gr";
            Recipe.Ingredient3 = Containers[2].Ingredient;
            Recipe.Amount3 = 0;
            Recipe.Type3 = "gr";
            Recipe.Ingredient4 = Containers[3].Ingredient;
            Recipe.Amount4 = 0;
            Recipe.Type4 = "tsp";
            Recipe.Ingredient5 = Containers[4].Ingredient;
            Recipe.Amount5 = 0;
            Recipe.Type5 = "tsp";
            Recipe.Ingredient6 = Containers[5].Ingredient;
            Recipe.Amount6 = 0;
            Recipe.Type6 = "tsp";
            Recipe.Ingredient7 = Containers[6].Ingredient;
            Recipe.Amount7 = 0;
            Recipe.Ingredient8 = Containers[7].Ingredient;
            Recipe.Amount8 = 0;
        }

        // Check if it is possible (if requested amounts are smaller than amounts in containers)
        // Each container which needs filling is added to the returned list
        public List<IngredientAmountToFill> CheckIfPossible()
        {
            CreateConvertedRecipe();
            List<IngredientAmountToFill> ingredients = new List<IngredientAmountToFill>();

            decimal diff = Containers[0].Amount - convertedRecipe.Amount1;
            if (diff < 0)
            {
                ingredients.Add(new IngredientAmountToFill()
                {
                    ProductName = convertedRecipe.Ingredient1,
                    AmountToFill = -diff
                });
            }

            diff = Containers[1].Amount - convertedRecipe.Amount2;
            if (diff < 0)
            {
                ingredients.Add(new IngredientAmountToFill()
                {
                    ProductName = convertedRecipe.Ingredient2,
                    AmountToFill = -diff
                });
            }

            diff = Containers[2].Amount - convertedRecipe.Amount3;
            if (diff < 0)
            {
                ingredients.Add(new IngredientAmountToFill()
                {
                    ProductName = convertedRecipe.Ingredient3,
                    AmountToFill = -diff
                });
            }

            diff = Containers[3].Amount - convertedRecipe.Amount4;
            if (diff < 0)
            {
                ingredients.Add(new IngredientAmountToFill()
                {
                    ProductName = convertedRecipe.Ingredient4,
                    AmountToFill = -diff
                });
            }

            diff = Containers[4].Amount - convertedRecipe.Amount5;
            if (diff < 0)
            {
                ingredients.Add(new IngredientAmountToFill()
                {
                    ProductName = convertedRecipe.Ingredient5,
                    AmountToFill = -diff
                });
            }

            diff = Containers[5].Amount - convertedRecipe.Amount6;
            if (diff < 0)
            {
                ingredients.Add(new IngredientAmountToFill()
                {
                    ProductName = convertedRecipe.Ingredient6,
                    AmountToFill = -diff
                });
            }

            diff = Containers[6].Amount - convertedRecipe.Amount7;
            if (diff < 0)
            {
                ingredients.Add(new IngredientAmountToFill()
                {
                    ProductName = convertedRecipe.Ingredient7,
                    AmountToFill = -diff
                });
            }

            diff = Containers[7].Amount - convertedRecipe.Amount8;
            if (diff < 0)
            {
                ingredients.Add(new IngredientAmountToFill()
                {
                    ProductName = convertedRecipe.Ingredient8,
                    AmountToFill = -diff
                });
            }

            return ingredients;
        }

        // Assumes that it is possible to perform the action
        // (checking if it is possible id done before calling the method)
        public bool LetsDoh()
        {
            SaveRecipe();

            CreateCommandRecipe();

            // Start execution - create command string and send it via bluetooth
            string command = CreateCommandString();
            // Start execution - send command via bluetooth
            if (DependencyService.Get<IBluetoothHelper>().IsConnected())
                DependencyService.Get<IBluetoothHelper>().WriteStringToDevice(command);
            else
                return false;

            // Need to wait for confirmation at the end of the execution

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

        // Creates a recipe that will be used to update the containers
        // meaning that the amounts of all ingredients are in grams
        private void CreateConvertedRecipe()
        {
            convertedRecipe = new UserRecipe();
            convertedRecipe.UserID = Recipe.UserID;
            convertedRecipe.LastUse = Recipe.LastUse;

            convertedRecipe.Ingredient1 = Recipe.Ingredient1;
            convertedRecipe.Amount1 = Recipe.Amount1;
            if (Recipe.Type1 != "gr")
                convertedRecipe.Amount1 = App.Ingredients.ConvertToGr(Recipe.Ingredient1, Recipe.Amount1, Recipe.Type1);
            convertedRecipe.Type1 = "gr";

            convertedRecipe.Ingredient2 = Recipe.Ingredient2;
            convertedRecipe.Amount2 = Recipe.Amount2;
            if (Recipe.Type2 != "gr")
                convertedRecipe.Amount2 = App.Ingredients.ConvertToGr(Recipe.Ingredient2, Recipe.Amount2, Recipe.Type2);
            convertedRecipe.Type2 = "gr";

            convertedRecipe.Ingredient3 = Recipe.Ingredient3;
            convertedRecipe.Amount3 = Recipe.Amount3;
            if (Recipe.Type3 != "gr")
                convertedRecipe.Amount3 = App.Ingredients.ConvertToGr(Recipe.Ingredient3, Recipe.Amount3, Recipe.Type3);
            convertedRecipe.Type3 = "gr";

            convertedRecipe.Ingredient4 = Recipe.Ingredient4;
            convertedRecipe.Amount4 = Recipe.Amount4;
            if (Recipe.Type4 != "gr")
                convertedRecipe.Amount4 = App.Ingredients.ConvertToGr(Recipe.Ingredient4, Recipe.Amount4, Recipe.Type4);
            convertedRecipe.Type4 = "gr";

            convertedRecipe.Ingredient5 = Recipe.Ingredient5;
            convertedRecipe.Amount5 = Recipe.Amount5;
            if (Recipe.Type5 != "gr")
                convertedRecipe.Amount5 = App.Ingredients.ConvertToGr(Recipe.Ingredient5, Recipe.Amount5, Recipe.Type5);
            convertedRecipe.Type5 = "gr";

            convertedRecipe.Ingredient6 = Recipe.Ingredient6;
            convertedRecipe.Amount6 = Recipe.Amount6;
            if (Recipe.Type6 != "gr")
                convertedRecipe.Amount6 = App.Ingredients.ConvertToGr(Recipe.Ingredient6, Recipe.Amount6, Recipe.Type6);
            convertedRecipe.Type6 = "gr";

            convertedRecipe.Ingredient7 = Recipe.Ingredient7;
            convertedRecipe.Amount7 = App.Ingredients.ConvertToMl(Recipe.Ingredient7, Recipe.Amount7);

            convertedRecipe.Ingredient8 = Recipe.Ingredient8;
            convertedRecipe.Amount8 = App.Ingredients.ConvertToMl(Recipe.Ingredient8, Recipe.Amount8);
        }

        // Creates a recipe that will be used to create the command to send to the machine
        // meaning that the amounts of the first 3 ingredients are in grams,
        // and the amount of the last 3 ingredients are in tsp
        private void CreateCommandRecipe()
        {
            commandRecipe = new UserRecipe();
            commandRecipe.UserID = Recipe.UserID;
            commandRecipe.LastUse = Recipe.LastUse;

            commandRecipe.Ingredient1 = Recipe.Ingredient1;
            commandRecipe.Amount1 = Recipe.Amount1;
            if (Recipe.Type1 != "gr")
                commandRecipe.Amount1 = App.Ingredients.ConvertToGr(Recipe.Ingredient1, Recipe.Amount1, Recipe.Type1);
            commandRecipe.Type1 = "gr";

            commandRecipe.Ingredient2 = Recipe.Ingredient2;
            commandRecipe.Amount2 = Recipe.Amount2;
            if (Recipe.Type2 != "gr")
                commandRecipe.Amount2 = App.Ingredients.ConvertToGr(Recipe.Ingredient2, Recipe.Amount2, Recipe.Type2);
            commandRecipe.Type2 = "gr";

            commandRecipe.Ingredient3 = Recipe.Ingredient3;
            commandRecipe.Amount3 = Recipe.Amount3;
            if (Recipe.Type3 != "gr")
                commandRecipe.Amount3 = App.Ingredients.ConvertToGr(Recipe.Ingredient3, Recipe.Amount3, Recipe.Type3);
            commandRecipe.Type3 = "gr";

            commandRecipe.Ingredient4 = Recipe.Ingredient4;
            commandRecipe.Amount4 = Recipe.Amount4;
            if (Recipe.Type4 != "tsp")
                commandRecipe.Amount4 = App.Ingredients.ConvertToTsp(Recipe.Ingredient4, Recipe.Amount4, Recipe.Type4);
            commandRecipe.Type4 = "tsp";

            commandRecipe.Ingredient5 = Recipe.Ingredient5;
            commandRecipe.Amount5 = Recipe.Amount5;
            if (Recipe.Type5 != "tsp")
                commandRecipe.Amount5 = App.Ingredients.ConvertToTsp(Recipe.Ingredient5, Recipe.Amount5, Recipe.Type5);
            commandRecipe.Type5 = "tsp";

            commandRecipe.Ingredient6 = Recipe.Ingredient6;
            commandRecipe.Amount6 = Recipe.Amount6;
            if (Recipe.Type6 != "tsp")
                commandRecipe.Amount6 = App.Ingredients.ConvertToTsp(Recipe.Ingredient6, Recipe.Amount6, Recipe.Type6);
            commandRecipe.Type6 = "tsp";

            // Water and Oil in user recipes are always in cup units (no need to convert)
        }

        private string CreateCommandString()
        {
            string command = "";

            command += "f1$" + ((int)(commandRecipe.Amount1)).ToString().PadLeft(3, '0') + ";";
            command += "f2$" + ((int)(commandRecipe.Amount2)).ToString().PadLeft(3, '0') + ";";
            command += "f3$" + ((int)(commandRecipe.Amount3)).ToString().PadLeft(3, '0') + ";";
            command += "f4$" + (commandRecipe.Amount4 / 0.25M).ToString().PadLeft(3, '0') + ";";
            command += "f5$" + (commandRecipe.Amount5 / 0.25M).ToString().PadLeft(3, '0') + ";";
            command += "f6$" + (commandRecipe.Amount6 / 0.25M).ToString().PadLeft(3, '0') + ";";
            command += "f7$" + (commandRecipe.Amount7 / 0.25M).ToString().PadLeft(3, '0') + ";";
            command += "f8$" + (commandRecipe.Amount8 / 0.25M).ToString().PadLeft(3, '0') + ";";
            command += "b;^";

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
            App.Containers.RemoveFromContainer(7, convertedRecipe.Amount7);
            App.Containers.RemoveFromContainer(8, convertedRecipe.Amount8);
        }
    }
}
