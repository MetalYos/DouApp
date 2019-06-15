using System;
using System.Collections.Generic;
using System.Text;

namespace DouApp.Models
{
    public class Ingredient
    {
        public string ProductName { get; set; }
        public string MeasuringType { get; set; }
        public decimal Tsp { get; set; }
        public decimal Tbsp { get; set; }
        public decimal Cup { get; set; }

        public Ingredient()
        {
            ProductName = "";
            MeasuringType = "gr";
        }

        public Ingredient(string name, string type = "gr")
        {
            ProductName = name;
            MeasuringType = type;
        }
    }
}
