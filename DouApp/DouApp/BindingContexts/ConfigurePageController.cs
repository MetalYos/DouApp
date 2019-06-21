using System;
using System.Collections.Generic;
using System.Text;

using DouApp.Models;

namespace DouApp.BindingContexts
{
    public class ContainerAndID
    {
        public Container Container { get; set; }
        public int IngredientIndex { get; set; }
        public decimal Amount { get; set; }
    }

    public class ConfigurePageController
    {
        public List<ContainerAndID> Containers { get; set; }
        public List<Ingredient> LargeIngredients { get; set; }
        public List<Ingredient> SmallIngredients { get; set; }

        public ConfigurePageController()
        {
            LargeIngredients = App.Ingredients.GetIngredientsByMeasuringType("gr");
            SmallIngredients = App.Ingredients.GetIngredientsByMeasuringType("tsp");

            List<Container> containers = App.Containers.GetContainers();
            Containers = new List<ContainerAndID>();
            foreach (var container in containers)
            {
                int index = App.Ingredients.GetIngredientIndexBySize(container.Ingredient, container.IsLarge);
                Containers.Add(new ContainerAndID
                {
                    Container = container,
                    IngredientIndex = index,
                    Amount = container.Amount
                });
            }
        }

        public void UpdateContainers()
        {
            for (int i = 0; i < Containers.Count; i++)
            {
                int index = Containers[i].IngredientIndex;
                if (!Containers[i].Container.IsLarge)
                    index += LargeIngredients.Count;

                App.Containers.UpdateContainerIngredient(i + 1, App.Ingredients.GetIngredient(index).ProductName);
                App.Containers.UpdateContainerAmount(i + 1, Containers[i].Amount);
            }

            App.Containers.SaveContainers();
        }
    }
}
