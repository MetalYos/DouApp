using System;
using System.Collections.Generic;
using System.Text;

namespace DouApp.Models
{
    public class Container
    {
        public int ID { get; set; }
        public string Ingredient { get; set; }
        public decimal Amount { get; set; }
        public bool IsLarge { get; set; }
        public string GenericName { get; set; }

        public Container()
        {
            ID = 0;
            Amount = 0;
            IsLarge = true;

            ConstructGenericName();
        }

        public Container(int id, string ingredient, decimal amount, bool isLarge)
        {
            ID = id;
            Ingredient = ingredient;
            Amount = amount;
            IsLarge = isLarge;

            ConstructGenericName();
        }

        public override string ToString()
        {
            return Ingredient;
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
