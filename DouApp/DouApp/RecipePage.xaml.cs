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
        public RecipePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext != null)
            {
                ingredientsListView.ItemsSource = (BindingContext as RecipePageController).Ingredients;
            }
        }

        async private void LetsDohRecipeButton_Clicked(object sender, EventArgs e)
        {
            var controller = BindingContext as RecipePageController;

            if (recipeNameEntry.Text == string.Empty)
            {
                await DisplayAlert("Error!", "Please enter a name before baking!", "Ok");
                return;
            }

            var ingredientsToFill = controller.CheckIfPossible();
            if (ingredientsToFill.Count > 0)
            {
                string message = "";
                foreach (var ingredient in ingredientsToFill)
                {
                    message += "Not enough content in " + ingredient.ProductName + " container to make doh!\n"
                            + "Please fill in " + (ingredient.AmountToFill).ToString() + " grams.\n";
                }
                await DisplayAlert("Error!", message, "Ok");
                await Navigation.PopAsync();
                return;
            }

            // Execute command
            await controller.LetsDoh(this);

            await Navigation.PopAsync();
        }

        async private void SaveRecipeButton_Clicked(object sender, EventArgs e)
        {
            if (recipeNameEntry.Text == string.Empty)
            {
                await DisplayAlert("Error!", "Please enter a name before saving!", "Ok");
                return;
            }

            var controller = BindingContext as RecipePageController;

            if (controller == null)
                return;

            controller.SaveRecipe();

            await Navigation.PopAsync();
        }
    }
}