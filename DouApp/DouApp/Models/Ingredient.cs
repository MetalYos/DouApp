using System;
using System.Collections.Generic;
using System.Text;

namespace DouApp.Models
{
    public class Ingredient
    {
        static int count = 0;

        public int ID { get; set; }
        public string ProductName { get; set; }
        public string Type { get; set; }

        public Ingredient()
        {
            ID = count++;
            ProductName = "";
            Type = "gr";
        }

        public Ingredient(string name, string type = "gr")
        {
            ID = count++;
            ProductName = name;
            Type = type;
        }
    }
}
