using System;
using System.Collections.Generic;
using System.Text;

namespace DouApp.Models
{
    public class UserRecipe
    {
        public int UserID { get; set; }
        public string RecipeName { get; set; }
        public DateTime LastUse { get; set; }
        public string Ingredient1 { get; set; }
        public decimal Amount1 { get; set; }
        public string Type1 { get; set; }
        public string Ingredient2 { get; set; }
        public decimal Amount2 { get; set; }
        public string Type2 { get; set; }
        public string Ingredient3 { get; set; }
        public decimal Amount3 { get; set; }
        public string Type3 { get; set; }
        public string Ingredient4 { get; set; }
        public decimal Amount4 { get; set; }
        public string Type4 { get; set; }
        public string Ingredient5 { get; set; }
        public decimal Amount5 { get; set; }
        public string Type5 { get; set; }
        public string Ingredient6 { get; set; }
        public decimal Amount6 { get; set; }
        public string Type6 { get; set; }
        public string Ingredient7 { get; set; }
        public decimal Amount7 { get; set; }
        public string Ingredient8 { get; set; }
        public decimal Amount8 { get; set; }

        public static int CompareByLastUse(UserRecipe a, UserRecipe b)
        {
            return DateTime.Compare(b.LastUse, a.LastUse);
        }
    }
}