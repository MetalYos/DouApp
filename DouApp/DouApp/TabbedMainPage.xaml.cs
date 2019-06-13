using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using DouApp.Models;
using DouApp.BindingContexts;
using DouApp.Databases;
using System.Collections.ObjectModel;

namespace DouApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedMainPage : TabbedPage
    {
        private ObservableCollection<UserRecipe> recipes;
        private ObservableCollection<UserRecipe> latestThreeRecipes;
        public TabbedMainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            recipes = App.RecipesDB.GetRecipesMock();
            Helpers.Sort(recipes, (a, b) => { return b.LastUse.CompareTo(a.LastUse); });

            latestThreeRecipes = new ObservableCollection<UserRecipe>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Show 3 latest recipes
            latestThreeRecipes.Clear();
            recipesListView.ItemsSource = null;
            for (int i = 0; i < Math.Min(3, recipes.Count); i++)
                latestThreeRecipes.Add(recipes[i]);
            recipesListView.ItemsSource = latestThreeRecipes;
            recipesListView.SelectedItem = null;

            // Show all recipes
            recipesHistoryListView.ItemsSource = null;
            recipesHistoryListView.ItemsSource = recipes;
            recipesHistoryListView.SelectedItem = null;

            // Show stats
            containersAmountListView.ItemsSource = null;
            containersAmountListView.ItemsSource = App.Containers.GetContainers();
        }

        async private void NewRecipeButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RecipePage
            {
                BindingContext = new RecipePageController(new UserRecipe(), true)
            });
        }

        private void DownloadRecipeButton_Clicked(object sender, EventArgs e)
        {

        }

        async private void RecipesListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                // Check first if it is compatitable to the current configuration
                UserRecipe recipe = (e.SelectedItem as UserRecipe);
                if (!CheckIfRecipeIsPossible(recipe))
                {
                    await DisplayAlert("Recipe not supported", "Current machine configuration does not support selected recipe", "OK");
                    return;
                }
                
                await Navigation.PushAsync(new RecipePage
                {
                    BindingContext = new RecipePageController(recipe, false)
                });
            }
        }

        private async void ConfigureContainersButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ConfigurePage
            {
                BindingContext = new ConfigurePageController(),
                FirstTime = false
            });
        }

        private bool CheckIfRecipeIsPossible(UserRecipe recipe)
        {
            bool possible = true;
            var containers = App.Containers.GetContainers();

            possible = possible && CheckIngredientInContainers(containers, recipe.Ingridient1);
            possible = possible && CheckIngredientInContainers(containers, recipe.Ingridient2);
            possible = possible && CheckIngredientInContainers(containers, recipe.Ingridient3);
            possible = possible && CheckIngredientInContainers(containers, recipe.Ingridient4);
            possible = possible && CheckIngredientInContainers(containers, recipe.Ingridient5);
            possible = possible && CheckIngredientInContainers(containers, recipe.Ingridient6);

            return possible;
        }

        private bool CheckIngredientInContainers(List<Container> containers, string ingredientName)
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
    }
}