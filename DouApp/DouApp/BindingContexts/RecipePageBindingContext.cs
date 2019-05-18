using System;
using System.Collections.Generic;
using System.Text;
using DouApp.Models;

namespace DouApp.BindingContexts
{
    class RecipePageBindingContext
    {
        public Recipe PageRecipe { get; private set; }
        public List<string> ContainerNames { get; private set; }

        public RecipePageBindingContext()
        {
            PageRecipe = new Recipe();
            ContainerNames = SetContainerNames();
        }

        public RecipePageBindingContext(Recipe recipe)
        {
            PageRecipe = recipe;
            ContainerNames = SetContainerNames();
        }

        private List<string> SetContainerNames()
        {
            List<string> names = new List<string>();
            List<Container> containers = App.Database.GetContainers();
            foreach (var container in containers)
            {
                names.Add(container.Name);
            }
            return names;
        }
    }
}
