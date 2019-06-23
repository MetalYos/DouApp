using System;
using System.Collections.Generic;
using System.Text;

namespace DouApp.Models
{

    public class ContainersToDatabase
    {
        public int UserID { get; set; }
        public string Ingredient1 { get; set; }
        public string Ingredient2 { get; set; }
        public string Ingredient3 { get; set; }
        public string Ingredient4 { get; set; }
        public string Ingredient5 { get; set; }
        public string Ingredient6 { get; set; }
        public string Ingredient7 { get; set; }
        public string Ingredient8 { get; set; }
        public decimal Amount1 { get; set; }
        public decimal Amount2 { get; set; }
        public decimal Amount3 { get; set; }
        public decimal Amount4 { get; set; }
        public decimal Amount5 { get; set; }
        public decimal Amount6 { get; set; }
        public decimal Amount7 { get; set; }
        public decimal Amount8 { get; set; }
    }

    public class ContainersAmounts
    {
        public int UserID { get; set; }
        public decimal Amount1 { get; set; }
        public decimal Amount2 { get; set; }
        public decimal Amount3 { get; set; }
        public decimal Amount4 { get; set; }
        public decimal Amount5 { get; set; }
        public decimal Amount6 { get; set; }
        public decimal Amount7 { get; set; }
        public decimal Amount8 { get; set; }
    }

    public class Container
    {
        public int ID { get; set; }
        public string Ingredient { get; set; }
        public decimal Amount { get; set; }
        public bool IsLarge { get; set; }
        public bool IsLiquid { get; set; }
        public string GenericName { get; set; }

        public Container()
        {
            ID = 0;
            Amount = 0;
            IsLarge = true;
            IsLiquid = false;

            ConstructGenericName();
        }

        public Container(int id, string ingredient, decimal amount, bool isLarge)
        {
            ID = id;
            Ingredient = ingredient;
            Amount = amount;
            IsLarge = isLarge;
            IsLiquid = false;

            ConstructGenericName();
        }

        public Container(int id, string ingredient, decimal amount, bool isLarge, bool isLiquid)
        {
            ID = id;
            Ingredient = ingredient;
            Amount = amount;
            IsLarge = isLarge;
            IsLiquid = isLiquid;

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
            if (IsLiquid)
                size = "Liquid";
            GenericName = "Container " + ID.ToString() + " (" + size + ")";
        }
    }
}
