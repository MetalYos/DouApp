using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

using DouApp.Models;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace DouApp.Databases
{
    public class ContainersDatabase
    {
        string setContainersUrl = @"https://dohconverter.azurewebsites.net/api/SetContainers";
        string getContainersUrl = @"https://dohconverter.azurewebsites.net/api/GetContainers";
        //string updateAmountsUrl;
        List<Container> containers;

        public ContainersDatabase()
        {
            LoadContainers();
        }

        private void LoadContainers()
        {
            ContainersToDatabase containersToDB = new ContainersToDatabase();
            containersToDB.UserID = App.UserID;

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(getContainersUrl);
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Method = "POST";
            httpWebRequest.Accept = "application/json; charset=utf-8";

            using (var reqStream = httpWebRequest.GetRequestStream())
            {
                using (var streamWriter = new StreamWriter(reqStream))
                {
                    string jsonOutput = JsonConvert.SerializeObject(containersToDB);

                    streamWriter.Write(jsonOutput);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    containersToDB = JsonConvert.DeserializeObject<ContainersToDatabase>(result.ToString());
                }
            }

            ContainersToList(containersToDB);
        }

        private void ContainersToList(ContainersToDatabase containersToDB)
        {
            containers = new List<Container>();

            if (containersToDB.Ingredient1 == null)
                containers.Add(new Container(1, "Cornflour", 0, true));
            else
                containers.Add(new Container(1, containersToDB.Ingredient1, 
                    containersToDB.Amount1, true));

            if (containersToDB.Ingredient2 == null)
                containers.Add(new Container(2, "Flour", 0, true));
            else
                containers.Add(new Container(2, containersToDB.Ingredient2, 
                    containersToDB.Amount2, true));

            if (containersToDB.Ingredient3 == null)
                containers.Add(new Container(3, "Poppyseed", 0, true));
            else
                containers.Add(new Container(3, containersToDB.Ingredient3, 
                    containersToDB.Amount3, true));

            if (containersToDB.Ingredient4 == null)
                containers.Add(new Container(4, "Salt", 0, false));
            else
                containers.Add(new Container(4, containersToDB.Ingredient4, 
                    containersToDB.Amount4, false));

            if (containersToDB.Ingredient5 == null)
                containers.Add(new Container(5, "Yeast", 0, false));
            else
                containers.Add(new Container(5, containersToDB.Ingredient5, 
                    containersToDB.Amount5, false));

            if (containersToDB.Ingredient6 == null)
                containers.Add(new Container(6, "Soda Powder", 0, false));
            else
                containers.Add(new Container(6, containersToDB.Ingredient6, 
                    containersToDB.Amount6, false));

            if (containersToDB.Ingredient7 == null)
                containers.Add(new Container(7, "Water", 0, false, true));
            else
                containers.Add(new Container(7, containersToDB.Ingredient7,
                    containersToDB.Amount7, false, true));

            if (containersToDB.Ingredient8 == null)
                containers.Add(new Container(8, "Oil", 0, false, true));
            else
                containers.Add(new Container(8, containersToDB.Ingredient8,
                    containersToDB.Amount8, false, true));
        }

        public bool SaveContainers()
        {
            ContainersToDatabase containersToDB = ListToContainers();

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(setContainersUrl);
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Method = "POST";
            httpWebRequest.Accept = "application/json; charset=utf-8";

            using (var reqStream = httpWebRequest.GetRequestStream())
            {
                using (var streamWriter = new StreamWriter(reqStream))
                {
                    string jsonOutput = JsonConvert.SerializeObject(containersToDB);

                    streamWriter.Write(jsonOutput);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                HttpStatusCode code = httpResponse.StatusCode;
                httpResponse.Close();

                return (code == HttpStatusCode.OK);
            }
        }

        private ContainersToDatabase ListToContainers()
        {
            ContainersToDatabase containersToDB = new ContainersToDatabase();
            containersToDB.UserID = App.UserID;
            containersToDB.Ingredient1 = containers[0].Ingredient;
            containersToDB.Amount1 = containers[0].Amount;
            containersToDB.Ingredient2 = containers[1].Ingredient;
            containersToDB.Amount2 = containers[1].Amount;
            containersToDB.Ingredient3 = containers[2].Ingredient;
            containersToDB.Amount3 = containers[2].Amount;
            containersToDB.Ingredient4 = containers[3].Ingredient;
            containersToDB.Amount4 = containers[3].Amount;
            containersToDB.Ingredient5 = containers[4].Ingredient;
            containersToDB.Amount5 = containers[4].Amount;
            containersToDB.Ingredient6 = containers[5].Ingredient;
            containersToDB.Amount6 = containers[5].Amount;
            containersToDB.Ingredient7 = containers[6].Ingredient;
            containersToDB.Amount7 = containers[6].Amount;
            containersToDB.Ingredient8 = containers[7].Ingredient;
            containersToDB.Amount8 = containers[7].Amount;

            return containersToDB;
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

        public void AddToContainer(int id, decimal amountToAdd)
        {
            GetContainer(id).Amount += amountToAdd;
        }

        public void UpdateContainerIngredient(int id, string ingredient)
        {
            GetContainer(id).Ingredient = ingredient;
        }
    }
}
