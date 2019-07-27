using System;
using System.Collections.Generic;
using System.Text;

using DouApp.Models;
using DouApp.Interfaces;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;
using System.Threading.Tasks;

namespace DouApp.BindingContexts
{
    // Represents a recipe in the list view in the RecipePage
    public class RecipeIngredientsList
    {
        public List<string> Ingredients { get; set; }
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
        #region Properties
        // Is the recipe new or one from history
        public bool IsNew { get; set; }
        public List<Container> Containers { get; set; }
        public List<string> LargeIngredients { get; set; }
        public List<string> SmallIngredients { get; set; }
        public UserRecipe Recipe { get; set; }
        public List<RecipeIngredientsList> Ingredients { get; set; }

        // User recipe that will be used to check amounts against containers
        public UserRecipe ConvertedRecipe { get; set; }

        // User recipe that will be used to build command string
        public UserRecipe CommandRecipe { get; set; }
        #endregion

        public RecipePageController(UserRecipe recipe, bool isNew = false)
        {
            Containers = App.Containers.GetContainers();
            LargeIngredients = App.Ingredients.GetIngredientsNamesByMeasuringType("gr");
            SmallIngredients = App.Ingredients.GetIngredientsNamesByMeasuringType("tsp");

            Recipe = recipe;
            IsNew = isNew;

            // TODO: If it is a new Recipe, select the ingredients that are in the containers
            // otherwise show them as is (after updating the recipe to be as the containers).

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
                Ingredients = LargeIngredients,
                ProductName = Recipe.Ingredient1,
                Amount = Recipe.Amount1,
                UnitsIndex = GetUnitsIndex(Recipe.Type1),
                IsLarge = true,
                IsLiquid = false
            });
            Ingredients.Add(new RecipeIngredientsList()
            {
                Ingredients = LargeIngredients,
                ProductName = Recipe.Ingredient2,
                Amount = Recipe.Amount2,
                UnitsIndex = GetUnitsIndex(Recipe.Type2),
                IsLarge = true,
                IsLiquid = false
            });
            Ingredients.Add(new RecipeIngredientsList()
            {
                Ingredients = LargeIngredients,
                ProductName = Recipe.Ingredient3,
                Amount = Recipe.Amount3,
                UnitsIndex = GetUnitsIndex(Recipe.Type3),
                IsLarge = true,
                IsLiquid = false
            });
            Ingredients.Add(new RecipeIngredientsList()
            {
                Ingredients = SmallIngredients,
                ProductName = Recipe.Ingredient4,
                Amount = Recipe.Amount4,
                UnitsIndex = GetUnitsIndex(Recipe.Type4),
                IsLarge = false,
                IsLiquid = false
            });
            Ingredients.Add(new RecipeIngredientsList()
            {
                Ingredients = SmallIngredients,
                ProductName = Recipe.Ingredient5,
                Amount = Recipe.Amount5,
                UnitsIndex = GetUnitsIndex(Recipe.Type5),
                IsLarge = false,
                IsLiquid = false
            });
            Ingredients.Add(new RecipeIngredientsList()
            {
                Ingredients = SmallIngredients,
                ProductName = Recipe.Ingredient6,
                Amount = Recipe.Amount6,
                UnitsIndex = GetUnitsIndex(Recipe.Type6),
                IsLarge = false,
                IsLiquid = false
            });
            Ingredients.Add(new RecipeIngredientsList()
            {
                Ingredients = null,
                ProductName = Recipe.Ingredient7,
                Amount = Recipe.Amount7,
                UnitsIndex = 0,
                IsLarge = false,
                IsLiquid = true
            });
            Ingredients.Add(new RecipeIngredientsList()
            {
                Ingredients = null,
                ProductName = Recipe.Ingredient8,
                Amount = Recipe.Amount8,
                UnitsIndex = 0,
                IsLarge = false,
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
        private void UpdateRecipe()
        {
            Recipe.Ingredient1 = Ingredients[0].ProductName;
            Recipe.Amount1 = Ingredients[0].Amount;
            Recipe.Type1 = GetUnitFromIndex(Ingredients[0].UnitsIndex, Ingredients[0].IsLarge);

            Recipe.Ingredient2 = Ingredients[1].ProductName;
            Recipe.Amount2 = Ingredients[1].Amount;
            Recipe.Type2 = GetUnitFromIndex(Ingredients[1].UnitsIndex, Ingredients[1].IsLarge);

            Recipe.Ingredient3 = Ingredients[2].ProductName;
            Recipe.Amount3 = Ingredients[2].Amount;
            Recipe.Type3 = GetUnitFromIndex(Ingredients[2].UnitsIndex, Ingredients[2].IsLarge);

            Recipe.Ingredient4 = Ingredients[3].ProductName;
            Recipe.Amount4 = Ingredients[3].Amount;
            Recipe.Type4 = GetUnitFromIndex(Ingredients[3].UnitsIndex, Ingredients[3].IsLarge);

            Recipe.Ingredient5 = Ingredients[4].ProductName;
            Recipe.Amount5 = Ingredients[4].Amount;
            Recipe.Type5 = GetUnitFromIndex(Ingredients[4].UnitsIndex, Ingredients[4].IsLarge);

            Recipe.Ingredient6 = Ingredients[5].ProductName;
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
        private List<IngredientAmountToFill> IsThereEnoughInContainers()
        {
            List<IngredientAmountToFill> ingredients = new List<IngredientAmountToFill>();

            decimal diff = Containers[0].Amount - ConvertedRecipe.Amount1;
            if (diff < 0)
            {
                ingredients.Add(new IngredientAmountToFill()
                {
                    ProductName = ConvertedRecipe.Ingredient1,
                    AmountToFill = -diff
                });
            }

            diff = Containers[1].Amount - ConvertedRecipe.Amount2;
            if (diff < 0)
            {
                ingredients.Add(new IngredientAmountToFill()
                {
                    ProductName = ConvertedRecipe.Ingredient2,
                    AmountToFill = -diff
                });
            }

            diff = Containers[2].Amount - ConvertedRecipe.Amount3;
            if (diff < 0)
            {
                ingredients.Add(new IngredientAmountToFill()
                {
                    ProductName = ConvertedRecipe.Ingredient3,
                    AmountToFill = -diff
                });
            }

            diff = Containers[3].Amount - ConvertedRecipe.Amount4;
            if (diff < 0)
            {
                ingredients.Add(new IngredientAmountToFill()
                {
                    ProductName = ConvertedRecipe.Ingredient4,
                    AmountToFill = -diff
                });
            }

            diff = Containers[4].Amount - ConvertedRecipe.Amount5;
            if (diff < 0)
            {
                ingredients.Add(new IngredientAmountToFill()
                {
                    ProductName = ConvertedRecipe.Ingredient5,
                    AmountToFill = -diff
                });
            }

            diff = Containers[5].Amount - ConvertedRecipe.Amount6;
            if (diff < 0)
            {
                ingredients.Add(new IngredientAmountToFill()
                {
                    ProductName = ConvertedRecipe.Ingredient6,
                    AmountToFill = -diff
                });
            }

            diff = Containers[6].Amount - ConvertedRecipe.Amount7;
            if (diff < 0)
            {
                ingredients.Add(new IngredientAmountToFill()
                {
                    ProductName = ConvertedRecipe.Ingredient7,
                    AmountToFill = -diff
                });
            }

            diff = Containers[7].Amount - ConvertedRecipe.Amount8;
            if (diff < 0)
            {
                ingredients.Add(new IngredientAmountToFill()
                {
                    ProductName = ConvertedRecipe.Ingredient8,
                    AmountToFill = -diff
                });
            }

            return ingredients;
        }
        public async Task<bool> LetsDoh(ContentPage page)
        {
            UpdateRecipe();

            // Check if ingredients in recipe match the ingredients in the Containers
            var ingredientsNotInContainers = GetIngredientsNotInContainers(Recipe);
            if (ingredientsNotInContainers.Count > 0)
            {
                // show a message and return false
                string message = "";
                foreach (var ingredient in ingredientsNotInContainers)
                {
                    message += ingredient + " is not in any of the containers!\n";
                }
                await page.DisplayAlert("Error!", message, "Ok");

                return false;
            }

            // Update recipe to match containers order
            Recipe = UpdateRecipeOrderToContainers(Recipe);

            // Create ConvertedRecipe
            CreateConvertedRecipe();

            // Check if amount in containers is enough
            // If not, show a message and return false
            var ingredientsToFill = IsThereEnoughInContainers();
            if (ingredientsToFill.Count > 0)
            {
                string message = "";
                foreach (var ingredient in ingredientsToFill)
                {
                    string units = "grams";
                    if (ingredient.ProductName == "Water" || ingredient.ProductName == "Oil")
                        units = "ml";
                    message += "Not enough content in " + ingredient.ProductName + " container to make doh!\n"
                            + "Please fill in " + (ingredient.AmountToFill).ToString() + " " + units + ".\n";
                }
                await page.DisplayAlert("Error!", message, "Ok");

                return false;
            }

            // Save Recipe to Database
            SaveRecipe(false);

            // Create CommandRecipe
            CreateCommandRecipe();

            // Create command string
            string command = CreateCommandString();

            // Send command via bluetooth
            if (DependencyService.Get<IBluetoothHelper>().IsConnected())
            {
                DependencyService.Get<IBluetoothHelper>().WriteStringToDevice(command);
                command = "";
            }
            else
            {
                await page.DisplayAlert("Error!", "Bluetooth device is not connected! Going back to recipe page", "Ok");
                return false;
            }

            // Return true
            return true;
        }

        public void SaveRecipe(bool updateRecipe = true)
        {
            if (updateRecipe)
                UpdateRecipe();

            Recipe.LastUse = DateTime.Now;

            if (IsNew)
                App.RecipesDB.AddRecipe(Recipe);
            else
                App.RecipesDB.UpdateRecipe(Recipe);
        }

        // Creates a recipe that will be used to update the containers
        // meaning that the amounts of all dry ingredients are in grams
        // and the liquid ingredients are in ml
        private void CreateConvertedRecipe()
        {
            ConvertedRecipe = new UserRecipe();
            ConvertedRecipe.UserID = Recipe.UserID;
            ConvertedRecipe.LastUse = Recipe.LastUse;

            ConvertedRecipe.Ingredient1 = Recipe.Ingredient1;
            ConvertedRecipe.Amount1 = Recipe.Amount1;
            if (Recipe.Type1 != "gr")
                ConvertedRecipe.Amount1 = App.Ingredients.ConvertToGr(Recipe.Ingredient1, Recipe.Amount1, Recipe.Type1);
            ConvertedRecipe.Type1 = "gr";

            ConvertedRecipe.Ingredient2 = Recipe.Ingredient2;
            ConvertedRecipe.Amount2 = Recipe.Amount2;
            if (Recipe.Type2 != "gr")
                ConvertedRecipe.Amount2 = App.Ingredients.ConvertToGr(Recipe.Ingredient2, Recipe.Amount2, Recipe.Type2);
            ConvertedRecipe.Type2 = "gr";

            ConvertedRecipe.Ingredient3 = Recipe.Ingredient3;
            ConvertedRecipe.Amount3 = Recipe.Amount3;
            if (Recipe.Type3 != "gr")
                ConvertedRecipe.Amount3 = App.Ingredients.ConvertToGr(Recipe.Ingredient3, Recipe.Amount3, Recipe.Type3);
            ConvertedRecipe.Type3 = "gr";

            ConvertedRecipe.Ingredient4 = Recipe.Ingredient4;
            ConvertedRecipe.Amount4 = Recipe.Amount4;
            if (Recipe.Type4 != "gr")
                ConvertedRecipe.Amount4 = App.Ingredients.ConvertToGr(Recipe.Ingredient4, Recipe.Amount4, Recipe.Type4);
            ConvertedRecipe.Type4 = "gr";

            ConvertedRecipe.Ingredient5 = Recipe.Ingredient5;
            ConvertedRecipe.Amount5 = Recipe.Amount5;
            if (Recipe.Type5 != "gr")
                ConvertedRecipe.Amount5 = App.Ingredients.ConvertToGr(Recipe.Ingredient5, Recipe.Amount5, Recipe.Type5);
            ConvertedRecipe.Type5 = "gr";

            ConvertedRecipe.Ingredient6 = Recipe.Ingredient6;
            ConvertedRecipe.Amount6 = Recipe.Amount6;
            if (Recipe.Type6 != "gr")
                ConvertedRecipe.Amount6 = App.Ingredients.ConvertToGr(Recipe.Ingredient6, Recipe.Amount6, Recipe.Type6);
            ConvertedRecipe.Type6 = "gr";

            ConvertedRecipe.Ingredient7 = Recipe.Ingredient7;
            ConvertedRecipe.Amount7 = App.Ingredients.ConvertToMl(Recipe.Ingredient7, Recipe.Amount7);

            ConvertedRecipe.Ingredient8 = Recipe.Ingredient8;
            ConvertedRecipe.Amount8 = App.Ingredients.ConvertToMl(Recipe.Ingredient8, Recipe.Amount8);
        }

        // Creates a recipe that will be used to create the command to send to the machine
        // meaning that the amounts of the first 3 ingredients are in grams,
        // and the amount of the last 3 ingredients are in tsp
        private void CreateCommandRecipe()
        {
            if (ConvertedRecipe == null)
                CreateConvertedRecipe();

            CommandRecipe = new UserRecipe();
            CommandRecipe.UserID = Recipe.UserID;
            CommandRecipe.LastUse = Recipe.LastUse;

            CommandRecipe.Ingredient1 = Recipe.Ingredient1;
            CommandRecipe.Amount1 = ConvertedRecipe.Amount1;
            CommandRecipe.Type1 = "gr";

            CommandRecipe.Ingredient2 = Recipe.Ingredient2;
            CommandRecipe.Amount2 = ConvertedRecipe.Amount2;
            CommandRecipe.Type2 = "gr";

            CommandRecipe.Ingredient3 = Recipe.Ingredient3;
            CommandRecipe.Amount3 = ConvertedRecipe.Amount3;
            CommandRecipe.Type3 = "gr";

            CommandRecipe.Ingredient4 = Recipe.Ingredient4;
            CommandRecipe.Amount4 = Recipe.Amount4;
            if (Recipe.Type4 != "tsp")
                CommandRecipe.Amount4 = App.Ingredients.ConvertToTsp(Recipe.Ingredient4, Recipe.Amount4, Recipe.Type4);
            CommandRecipe.Type4 = "tsp";

            CommandRecipe.Ingredient5 = Recipe.Ingredient5;
            CommandRecipe.Amount5 = Recipe.Amount5;
            if (Recipe.Type5 != "tsp")
                CommandRecipe.Amount5 = App.Ingredients.ConvertToTsp(Recipe.Ingredient5, Recipe.Amount5, Recipe.Type5);
            CommandRecipe.Type5 = "tsp";

            CommandRecipe.Ingredient6 = Recipe.Ingredient6;
            CommandRecipe.Amount6 = Recipe.Amount6;
            if (Recipe.Type6 != "tsp")
                CommandRecipe.Amount6 = App.Ingredients.ConvertToTsp(Recipe.Ingredient6, Recipe.Amount6, Recipe.Type6);
            CommandRecipe.Type6 = "tsp";

            CommandRecipe.Ingredient7 = Recipe.Ingredient7;
            CommandRecipe.Amount7 = Recipe.Amount7;

            CommandRecipe.Ingredient8 = Recipe.Ingredient8;
            CommandRecipe.Amount8 = Recipe.Amount8;
        }

        // Checks for each ingredient that is in the recipe if it is in one of the containers or not
        // Returns all the recipe ingredients that are not in any of the containers
        private List<string> GetIngredientsNotInContainers(UserRecipe recipe)
        {
            List<string> ingredientsNotInContainers = new List<string>();
            var containers = App.Containers.GetContainers();

            if (!IsIngredientInContainers(containers, recipe.Ingredient1))
                ingredientsNotInContainers.Add(recipe.Ingredient1);

            if (!IsIngredientInContainers(containers, recipe.Ingredient2))
                ingredientsNotInContainers.Add(recipe.Ingredient2);

            if (!IsIngredientInContainers(containers, recipe.Ingredient3))
                ingredientsNotInContainers.Add(recipe.Ingredient3);

            if (!IsIngredientInContainers(containers, recipe.Ingredient4))
                ingredientsNotInContainers.Add(recipe.Ingredient4);

            if (!IsIngredientInContainers(containers, recipe.Ingredient5))
                ingredientsNotInContainers.Add(recipe.Ingredient5);

            if (!IsIngredientInContainers(containers, recipe.Ingredient6))
                ingredientsNotInContainers.Add(recipe.Ingredient6);

            return ingredientsNotInContainers;
        }

        // Returns true if the given ingredient is in one of the containers
        private bool IsIngredientInContainers(List<Container> containers, string ingredientName)
        {
            bool ingredientIn = false;
            foreach (var container in containers)
            {
                if (container.Ingredient == ingredientName)
                {
                    ingredientIn = true;
                    break;
                }
            }

            return ingredientIn;
        }

        private string CreateCommandString()
        {
            string command = "";

            // Division by 0.25 is done to containers that release 0.25 cup/spoon each time
            // order of containers on the machine is 4 -> 1 -> 5 -> 2 -> 6 -> 7 -> 8
            command += "f4$" + ((int)(CommandRecipe.Amount4 / 0.25M)).ToString().PadLeft(3, '0') + ";";
            command += "f1$" + ((int)(CommandRecipe.Amount1)).ToString().PadLeft(3, '0') + ";";
            command += "f5$" + ((int)(CommandRecipe.Amount5 / 0.25M)).ToString().PadLeft(3, '0') + ";";
            command += "f2$" + ((int)(CommandRecipe.Amount2)).ToString().PadLeft(3, '0') + ";";
            command += "f6$" + ((int)(CommandRecipe.Amount6 / 0.25M)).ToString().PadLeft(3, '0') + ";";
            command += "f7$" + ((int)(CommandRecipe.Amount7 / 0.25M)).ToString().PadLeft(3, '0') + ";";
            command += "f8$" + ((int)(CommandRecipe.Amount8 / 0.25M)).ToString().PadLeft(3, '0') + ";";
            //command += "f3$" + ((int)(commandRecipe.Amount3)).ToString().PadLeft(3, '0') + ";";
            command += "b;^";

            return command;
        }
    }
}
