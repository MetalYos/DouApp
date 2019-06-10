using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using DouApp.Models;
using DouApp.BindingContexts;

namespace DouApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipePage : ContentPage
    {
        public bool IsNew { get; set; }

        public RecipePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext != null)
                ingredientsListView.ItemsSource = (BindingContext as RecipePageBindingContext).PageRecipe.Stations;
        }

        async private void BakeRecipeButton_Clicked(object sender, EventArgs e)
        {
            if (recipeNameEntry.Text == string.Empty)
            {
                await DisplayAlert("Error!", "Please enter a name before baking!", "Ok");
                return;
            }

            Recipe recipe = (BindingContext as RecipePageBindingContext).PageRecipe;
            if (recipe == null)
            {
                await DisplayAlert("Error!", "Recipe is NULL!", "Ok");
                return;
            }

            // Save Recipe
            if (IsNew)
                App.Database.AddRecipe(recipe);
            else
                App.Database.UpdateRecipe(recipe);

            // Remove the baked amount from containers
            foreach (var item in recipe.Stations)
            {
                if (item.Container.IsLarge)
                {
                    if (!App.Containers.RemoveFromContainer(item.Container.ID, item.Weight))
                    {
                        await DisplayAlert("Error!", "Not enough content in " + item.Container.Ingredient.ProductName + " container to bake!\n"
                            + "Please fill in " + (item.Weight - item.Container.Amount).ToString() + " grams.", "Ok");
                        await Navigation.PopAsync();
                    }
                }
            }

            await Navigation.PopAsync();
        }

        private void AddIngredientButton_Clicked(object sender, EventArgs e)
        {
            Station station = new Station();
            station.SetLargeContainer(App.Containers.GetContainer(1), 500.0);
            (BindingContext as RecipePageBindingContext).PageRecipe.Stations.Add(station);
        }

        private void ContainerPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;
            Container container = App.Containers.GetContainer(selectedIndex + 1);
            Station station = ((picker.Parent as Grid).Parent as ViewCell).BindingContext as Station;
            var stations = (BindingContext as RecipePageBindingContext).PageRecipe.Stations;

            if (station == null)
                return;

            if (station.Container.ID != container.ID)
            {
                int index = stations.IndexOf(station);
                station.Container = container;
                stations[index] = station;
            }
        }

        private void RemoveStationButton_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            Station station = ((button.Parent as Grid).Parent as ViewCell).BindingContext as Station;
            (BindingContext as RecipePageBindingContext).PageRecipe.Stations.Remove(station);
        }
    }
}