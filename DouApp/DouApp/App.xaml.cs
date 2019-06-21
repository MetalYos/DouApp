using System;
using Xamarin.Forms;

using DouApp.Databases;

namespace DouApp
{
    public class NegateBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !(bool)value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !(bool)value;
        }
    }

    public partial class App : Application
    {
        // User ID
        public static int UserID { get; set; }

        // Bluetooth Device Name
        public static string DeviceName { get; set;  }


        // Databases
        static MockData database = null;
        static UsersDatabase userDatabase = null;
        static RecipesDatabase recipesDatabase = null;
        static IngredientsDatabase ingredientsDatabase = null;
        static ContainersDatabase containersDatabase = null;

        public static MockData Database
        {
            get
            {
                if (database == null)
                {
                    database = new MockData();
                }
                return database;
            }
        }

        public static UsersDatabase Users
        {
            get
            {
                if (userDatabase == null)
                {
                    userDatabase = new UsersDatabase();
                }
                return userDatabase;
            }
        }

        public static RecipesDatabase RecipesDB
        {
            get
            {
                if (recipesDatabase == null)
                {
                    recipesDatabase = new RecipesDatabase();
                }
                return recipesDatabase;
            }
        }

        public static IngredientsDatabase Ingredients
        {
            get
            {
                if (ingredientsDatabase == null)
                {
                    ingredientsDatabase = new IngredientsDatabase();
                }
                return ingredientsDatabase;
            }
        }

        public static ContainersDatabase Containers
        {
            get
            {
                if (containersDatabase == null)
                {
                    containersDatabase = new ContainersDatabase();
                }
                return containersDatabase;
            }
        }

        public App()
        {
            InitializeComponent();

            UserID = 0;
            DeviceName = "Sharon";

            MainPage = new NavigationPage(new LoginPage())
            {
                BarBackgroundColor = Color.FromHex("#002060")
            };
        }

        protected override void OnStart()
        {
            // Handle when your app starts

            // Load ingredients conversion table at start up
            Ingredients.LoadTable();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
