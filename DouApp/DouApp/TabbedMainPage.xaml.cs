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
    public partial class TabbedMainPage : TabbedPage
    {
        public TabbedMainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Show 3 latest recipes
            var recipes = App.Database.GetRecipes();
            recipes.Sort(new RecipeComparer());
            recipes.Reverse();
            recipesListView.ItemsSource = null;
            recipesListView.ItemsSource = recipes.GetRange(0, 3);

            // Show all recipes
            recipesHistoryListView.ItemsSource = null;
            recipesHistoryListView.ItemsSource = recipes;

            // Show stats
            containersAmountListView.ItemsSource = null;
            containersAmountListView.ItemsSource = App.Containers.GetContainers();
        }

        async private void NewRecipeButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RecipePage
                {
                BindingContext = new RecipePageBindingContext(new Recipe()),
                IsNew = true
            });
        }

        private void DownloadRecipeButton_Clicked(object sender, EventArgs e)
        {

        }

        async private void RecipesListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {

                await Navigation.PushAsync(new RecipePage
                {
                    BindingContext = new RecipePageBindingContext(e.SelectedItem as Recipe),
                    IsNew = false
                });
            }
        }

        private async void ConfigureContainersButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ConfigurePage
            {
                FirstTime = false,
                BindingContext = new ConfigurePageController()
            });
        }
    }
}