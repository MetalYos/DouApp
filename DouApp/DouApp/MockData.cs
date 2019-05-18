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

    public class RecipeComparer : IComparer<Recipe>
    {
        public int Compare(Recipe x, Recipe y)
        {
            if (x.Date < y.Date)
                return -1;
            if (x.Date > y.Date)
                return 1;

            return 0;
        }
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

    public class Container
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public bool IsLarge { get; set; }

        public Container()
        {
            ID = 0;
            Name = "";
            Amount = 0.0;
            IsLarge = true;
        }

        public Container(int id, string name, double amount, bool isLarge)
        {
            ID = id;
            Name = name;
            Amount = amount;
            IsLarge = isLarge;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class MockData
    {
        private List<Recipe> recipes;
        private List<Container> containers;

        public MockData()
        {
            recipes = new List<Recipe>();
            containers = new List<Container>();
            PopulateContainers();
            PopulateRecipes();
        }

        private void PopulateContainers()
        {
            /*
            containers.Add(new Container(0, "Flour", 750, true));
            containers.Add(new Container(1, "Sugar", 500, true));
            containers.Add(new Container(2, "Baking Powder", 750, true));
            containers.Add(new Container(3, "Cinnamon", 25, false));
            containers.Add(new Container(4, "Other", 45, false));
            containers.Add(new Container(5, "Other2", 30, false));
            */
            containers.Add(new Container(0, "Large1", 500, true));
            containers.Add(new Container(1, "Large2", 500, true));
            containers.Add(new Container(2, "Large3", 500, true));
            containers.Add(new Container(3, "Small1", 50, false));
            containers.Add(new Container(4, "Small2", 50, false));
            containers.Add(new Container(5, "Small3", 50, false));
        }

        public List<Container> GetContainers()
        {
            return containers;
        }

        public Container GetContainer(int id)
        {
            if (id >= 0 && id < containers.Count)
                return containers[id];
            return null;
        }

        public void UpdateContainerAmount(int id, double amount)
        {
            containers[id].Amount = amount;
        }

        public bool RemoveFromContainer(int id, double amountToRemove)
        {
            if (containers[id].Amount - amountToRemove < 0)
                return false;

            containers[id].Amount -= amountToRemove;
            return true;
        }

        public void UpdateContainerName(int id, string name)
        {
            containers[id].Name = name;
        }

        private void PopulateRecipes()
        {
            // Recipe 1
            ObservableCollection<Station> stations = new ObservableCollection<Station>();
            Station station = new Station();
            station.SetLargeContainer(GetContainer(0), 500);
            stations.Add(station);

            station = new Station();
            station.SetLargeContainer(GetContainer(1), 200);
            stations.Add(station);

            station = new Station();
            station.SetSmallContainer(GetContainer(3), (int)Spoons.Half);
            stations.Add(station);

            station = new Station();
            station.SetSmallContainer(GetContainer(5), (int)Spoons.Quarter);
            stations.Add(station);

            AddRecipe("Cupcake", stations);

            // Recipe 2
            stations = new ObservableCollection<Station>();
            station = new Station();
            station.SetLargeContainer(GetContainer(2), 500);
            stations.Add(station);

            station = new Station();
            station.SetLargeContainer(GetContainer(1), 400);
            stations.Add(station);

            station = new Station();
            station.SetSmallContainer(GetContainer(3), (int)Spoons.Half);
            stations.Add(station);

            station = new Station();
            station.SetSmallContainer(GetContainer(4), (int)Spoons.Full);
            stations.Add(station);

            station = new Station();
            station.SetSmallContainer(GetContainer(5), (int)Spoons.Quarter);
            stations.Add(station);

            AddRecipe("Brownie", stations);

            // Recipe 3
            stations = new ObservableCollection<Station>();
            station = new Station();
            station.SetLargeContainer(GetContainer(1), 300);
            stations.Add(station);

            station = new Station();
            station.SetSmallContainer(GetContainer(4), (int)Spoons.Half);
            stations.Add(station);

            station = new Station();
            station.SetSmallContainer(GetContainer(3), (int)Spoons.Full);
            stations.Add(station);

            station = new Station();
            station.SetSmallContainer(GetContainer(5), (int)Spoons.Half);
            stations.Add(station);

            AddRecipe("Chocolate", stations);

            // Recipe 4
            stations = new ObservableCollection<Station>();
            station = new Station();
            station.SetLargeContainer(GetContainer(0), 700);
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
    }
}
