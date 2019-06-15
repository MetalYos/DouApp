using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using DouApp.Models;

namespace DouApp
{
    public enum Spoons
    {
        Quarter,
        Half,
        Full
    }

    public class ContainerComparerByID : IComparer<Container>
    {
        public int Compare(Container x, Container y)
        {
            if (x.ID < y.ID)
                return -1;
            if (x.ID > y.ID)
                return 1;
            else
                return 0;
        }
    }

    public class MockData
    {
        /*
        private List<Recipe> recipes;
        private List<Container> containers;

        public MockData()
        {
            recipes = new List<Recipe>();
            containers = new List<Container>();
            PopulateRecipes();
        }

        private void PopulateRecipes()
        {
            // Recipe 1
            ObservableCollection<Station> stations = new ObservableCollection<Station>();
            Station station = new Station();
            station.SetLargeContainer(App.Containers.GetContainer(1), 500);
            stations.Add(station);

            station = new Station();
            station.SetLargeContainer(App.Containers.GetContainer(2), 200);
            stations.Add(station);

            station = new Station();
            station.SetSmallContainer(App.Containers.GetContainer(4), (int)Spoons.Half);
            stations.Add(station);

            station = new Station();
            station.SetSmallContainer(App.Containers.GetContainer(6), (int)Spoons.Quarter);
            stations.Add(station);

            AddRecipe("Cupcake", stations);

            // Recipe 2
            stations = new ObservableCollection<Station>();
            station = new Station();
            station.SetLargeContainer(App.Containers.GetContainer(3), 500);
            stations.Add(station);

            station = new Station();
            station.SetLargeContainer(App.Containers.GetContainer(2), 400);
            stations.Add(station);

            station = new Station();
            station.SetSmallContainer(App.Containers.GetContainer(4), (int)Spoons.Half);
            stations.Add(station);

            station = new Station();
            station.SetSmallContainer(App.Containers.GetContainer(5), (int)Spoons.Full);
            stations.Add(station);

            station = new Station();
            station.SetSmallContainer(App.Containers.GetContainer(6), (int)Spoons.Quarter);
            stations.Add(station);

            AddRecipe("Brownie", stations);

            // Recipe 3
            stations = new ObservableCollection<Station>();
            station = new Station();
            station.SetLargeContainer(App.Containers.GetContainer(2), 300);
            stations.Add(station);

            station = new Station();
            station.SetSmallContainer(App.Containers.GetContainer(5), (int)Spoons.Half);
            stations.Add(station);

            station = new Station();
            station.SetSmallContainer(App.Containers.GetContainer(4), (int)Spoons.Full);
            stations.Add(station);

            station = new Station();
            station.SetSmallContainer(App.Containers.GetContainer(6), (int)Spoons.Half);
            stations.Add(station);

            AddRecipe("Chocolate", stations);

            // Recipe 4
            stations = new ObservableCollection<Station>();
            station = new Station();
            station.SetLargeContainer(App.Containers.GetContainer(1), 700);
            stations.Add(station);

            AddRecipe("Flour", stations);
        }

        public List<Recipe> GetRecipes()
        {
            return recipes;
        }

        public Recipe GetRecipe(int id)
        {
            foreach (var recipe in recipes)
            {
                if (recipe.ID == id)
                {
                    return recipe;
                }
            }
            return null;
        }

        public void AddRecipe(string name, ObservableCollection<Station> stations)
        {
            Recipe recipe = new Recipe();
            recipe.Name = name;
            recipe.Stations = stations;

            Random random = new Random(DateTime.Now.Millisecond);
            int numDays = random.Next(-recipe.ID * 5, 0);
            recipe.Date = DateTime.Now.Date.AddDays((double)numDays);

            recipes.Add(recipe);
        }

        public void AddRecipe(Recipe recipe)
        {
            recipe.Date = DateTime.Now.Date;
            recipes.Add(recipe);
        }

        public bool RemoveRecipe(int id)
        {
            foreach (var recipe in recipes)
            {
                if (recipe.ID == id)
                {
                    recipes.Remove(recipe);
                    return true;
                }
            }
            return false;
        }

        public bool UpdateRecipe(Recipe updated)
        {
            Recipe recipe = GetRecipe(updated.ID);
            if (recipe == null)
                return false;

            int index = recipes.IndexOf(recipe);
            recipes.Remove(recipe);
            recipes.Insert(index, recipe);
            return true;
        }

        public bool ContainsRecipe(Recipe recipe)
        {
            foreach (var item in recipes)
            {
                if (recipe.ID == item.ID)
                    return true;
            }
            return false;
        }
        */
    }
}
