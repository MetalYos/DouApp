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
            if (controller == null)
                return;

            if (recipeNameEntry.Text == string.Empty)
            {
                await DisplayAlert("Error!", "Please enter a name before baking!", "Ok");
                return;
            }

            bool canContinue = await controller.LetsDoh(this);

            // Move on to the Progress Page
            if (canContinue)
            {
                await Navigation.PushAsync(new ProgressPage()
                {
                    ConvertedRecipe = controller.ConvertedRecipe,
                    CommandRecipe = controller.CommandRecipe
                });
            }
        }

        async private void SaveRecipeButton_Clicked(object sender, EventArgs e)
        {
            var controller = BindingContext as RecipePageController;
            if (controller == null)
                return;

            if (recipeNameEntry.Text == string.Empty)
            {
                await DisplayAlert("Error!", "Please enter a name before saving!", "Ok");
                return;
            }

            controller.SaveRecipe();

            await Navigation.PopAsync();
        }
    }
}