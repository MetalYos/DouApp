using System;
using System.Collections.Generic;
using System.Text;

using DouApp.Models;

namespace DouApp.BindingContexts
{
    class AddToContainer
    {
        public string GenericName { get; set; }
        public decimal CurrentAmount { get; set; }
        public decimal AmountToAdd { get; set; }
        public bool IsLiquid { get; set; }
    }

    class AddToContainersPageController
    {
        public List<AddToContainer> Containers { get; set; }

        public AddToContainersPageController()
        {
            List<Container> containers = App.Containers.GetContainers();
            Containers = new List<AddToContainer>();
            foreach (var container in containers)
            {
                Containers.Add(new AddToContainer
                {
                    GenericName = container.GenericName,
                    CurrentAmount = container.Amount,
                    AmountToAdd = 0,
                    IsLiquid = container.IsLiquid
                });
            }
        }

        public void UpdateContainers()
        {
            for (int i = 0; i < Containers.Count; i++)
            {
                App.Containers.AddToContainer(i + 1, Containers[i].AmountToAdd);
            }

            App.Containers.SaveContainers();
        }
    }
}
