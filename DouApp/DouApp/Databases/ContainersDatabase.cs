using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using DouApp.Models;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace DouApp.Databases
{
    public class ContainersDatabase
    {
        string setContainersUrl;
        string getContainersUrl;
        string updateAmountsUrl;
        List<Container> containers;
        string name = "containers.txt";

        public ContainersDatabase()
        {
            LoadContainers();
        }

        private void LoadContainers()
        {
            containers = new List<Container>();
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string filePath = Path.Combine(path, name);

            if (!File.Exists(filePath))
            {
                containers.Add(new Container(1, "Cornflour", 0, true));
                containers.Add(new Container(2, "Flour", 0, true));
                containers.Add(new Container(3, "Poppyseed", 0, true));
                containers.Add(new Container(4, "Salt", 0, false));
                containers.Add(new Container(5, "Yeast", 0, false));
                containers.Add(new Container(6, "Soda Powder", 0, false));
            }
            else
            {
                using (var file = File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var strm = new StreamReader(file))
                    {
                        string json = strm.ReadToEnd();
                        containers = JsonConvert.DeserializeObject<List<Container>>(json);
                    }
                }
            }
        }

        public void SaveContainers()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string filePath = Path.Combine(path, name);

            using (var file = File.Open(filePath, FileMode.Create, FileAccess.Write))
            {
                string json = JsonConvert.SerializeObject(containers);

                using (var strm = new StreamWriter(file))
                {
                    strm.Write(json);
                }
            }
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

        public void UpdateContainerAmount(int id, decimal amount)
        {
            GetContainer(id).Amount = amount;
        }

        public bool RemoveFromContainer(int id, decimal amountToRemove)
        {
            if (GetContainer(id).Amount - amountToRemove < 0)
                return false;

            GetContainer(id).Amount -= amountToRemove;
            return true;
        }

        public void UpdateContainerIngredient(int id, string ingredient)
        {
            GetContainer(id).Ingredient = ingredient;
        }
    }
}
