using System;
using System.Collections.Generic;
using System.Text;

using DouApp.Models;

namespace DouApp.BindingContexts
{
    public class ContainerAndID
    {
        public Container Container { get; set; }
        public int ID { get; set; }
    }

    public class ConfigurePageController
    {
        public List<ContainerAndID> Containers { get; set; }
        public List<Ingredient> LargeIngredients { get; set; }
        public List<Ingredient> SmallIngredients { get; set; }

        public ConfigurePageController()
        {
            LargeIngredients = App.Ingredients.GetIngredientsByType("gr");
            SmallIngredients = App.Ingredients.GetIngredientsByType("tsp");

            List<Container> containers = App.Containers.GetContainers();
            Containers = new List<ContainerAndID>();
            foreach (var container in containers)
            {
                int id = (container.IsLarge) ? container.Ingredient.ID : container.Ingredient.ID - LargeIngredients.Count;
                Containers.Add(new ContainerAndID
                {
                    Container = container,
                    ID = id
                });
            }
        }

        public void UpdateContainers()
        {
            for (int i = 0; i < Containers.Count; i++)
            {
                int id = Containers[i].ID;
                if (!Containers[i].Container.IsLarge)
                    id += LargeIngredients.Count;

                if (Containers[i].Container.Ingredient.ID == id)
                    continue;

                App.Containers.UpdateContainerIngredient(i + 1, App.Ingredients.GetIngredient(id));
            }
        }
    }
}
