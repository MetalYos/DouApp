using System;
using System.Collections.Generic;
using System.Text;

using DouApp.Models;

namespace DouApp.BindingContexts
{
    public class ContainerForView
    {
        public Container Container { get; set; }
        public Ingredient Ingredient{ get; set; }
        public decimal Amount { get; set; }
    }

    public class ConfigurePageController
    {
        public List<ContainerForView> Containers { get; set; }
        public List<Ingredient> LargeIngredients { get; set; }
        public List<Ingredient> SmallIngredients { get; set; }

        public ConfigurePageController()
        {
            LargeIngredients = App.Ingredients.GetIngredientsByMeasuringType("gr");
            SmallIngredients = App.Ingredients.GetIngredientsByMeasuringType("tsp");

            List<Container> containers = App.Containers.GetContainers();
            Containers = new List<ContainerForView>();
            foreach (var container in containers)
            {
                Containers.Add(new ContainerForView
                {
                    Container = container,
                    Ingredient = App.Ingredients.GetIngredient(container.Ingredient),
                    Amount = container.Amount
                });
            }
        }

        public void UpdateContainers()
        {
            for (int i = 0; i < Containers.Count; i++)
            {
                Ingredient ingredient = Containers[i].Ingredient;

                App.Containers.UpdateContainerIngredient(i + 1, ingredient.ProductName);
                App.Containers.UpdateContainerAmount(i + 1, Containers[i].Amount);
            }

            App.Containers.SaveContainers();
        }
    }
}
