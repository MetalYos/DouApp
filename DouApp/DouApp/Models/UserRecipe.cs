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
        public string Ingridient1 { get; set; }
        public decimal Amount1 { get; set; }
        public string Type1 { get; set; }
        public string Ingridient2 { get; set; }
        public decimal Amount2 { get; set; }
        public string Type2 { get; set; }
        public string Ingridient3 { get; set; }
        public decimal Amount3 { get; set; }
        public string Type3 { get; set; }
        public string Ingridient4 { get; set; }
        public decimal Amount4 { get; set; }
        public string Type4 { get; set; }
        public string Ingridient5 { get; set; }
        public decimal Amount5 { get; set; }
        public string Type5 { get; set; }
        public string Ingridient6 { get; set; }
        public decimal Amount6 { get; set; }
        public string Type6 { get; set; }
    }
}