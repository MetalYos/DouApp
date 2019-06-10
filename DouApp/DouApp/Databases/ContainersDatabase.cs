using System;
using System.Collections.Generic;
using System.Text;

using DouApp.Models;

namespace DouApp.Databases
{
    public class ContainersDatabase
    {
        List<Container> containers;

        public ContainersDatabase()
        {
            containers = new List<Container>();
            containers.Add(new Container(1, App.Ingredients.GetIngredient("Flour"), 0.0, true));
            containers.Add(new Container(2, App.Ingredients.GetIngredient("White Sugar"), 0.0, true));
            containers.Add(new Container(3, App.Ingredients.GetIngredient("Almonds"), 0.0, true));
            containers.Add(new Container(4, App.Ingredients.GetIngredient("Salt"), 0.0, false));
            containers.Add(new Container(5, App.Ingredients.GetIngredient("Yeast"), 0.0, false));
            containers.Add(new Container(6, App.Ingredients.GetIngredient("Cinnamon"), 0.0, false));
        }

        public Container GetContainer(int id)
        {
            int index = (id - 1) % containers.Count;
            return containers[index];
        }

        public List<Container> GetContainers()
        {
            return containers;
        }

        public List<Container> GetContainersBySize(bool isLarge)
        {
            List<Container> containersToReturn = new List<Container>();

            foreach (var container in containers)
            {
                if (container.IsLarge == isLarge)
                    containersToReturn.Add(container);
            }

            return containersToReturn;
        }

        public void UpdateContainerAmount(int id, double amount)
        {
            GetContainer(id).Amount = amount;
        }

        public bool RemoveFromContainer(int id, double amountToRemove)
        {
            if (GetContainer(id).Amount - amountToRemove < 0)
                return false;

            GetContainer(id).Amount -= amountToRemove;
            return true;
        }

        public void UpdateContainerIngredient(int id, Ingredient ingredient)
        {
            GetContainer(id).Ingredient = ingredient;
        }
    }
}
