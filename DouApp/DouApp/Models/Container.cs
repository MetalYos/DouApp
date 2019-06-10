using System;
using System.Collections.Generic;
using System.Text;

namespace DouApp.Models
{
    public class Container
    {
        public int ID { get; set; }
        public Ingredient Ingredient { get; set; }
        public double Amount { get; set; }
        public bool IsLarge { get; set; }
        public string GenericName { get; set; }

        public Container()
        {
            ID = 0;
            Amount = 0.0;
            IsLarge = true;

            ConstructGenericName();
        }

        public Container(int id, Ingredient ingredient, double amount, bool isLarge)
        {
            ID = id;
            Ingredient = ingredient;
            Amount = amount;
            IsLarge = isLarge;

            ConstructGenericName();
        }

        public override string ToString()
        {
            return Ingredient.ProductName;
        }

        private void ConstructGenericName()
        {
            string size = "Large";
            if (!IsLarge)
                size = "Small";
            GenericName = "Container " + ID.ToString() + " (" + size + ")";
        }
    }
}
