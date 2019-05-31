using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
        static int userID = 0;
        public static int UserID { get; set; }


        // Databases
        static MockData database = null;
        static UsersDatabase userDatabase = null;

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

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
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
