using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DouApp.Models
{
    public class Recipe
    {
        // Remove once we are using SQL databases
        private static int counter = 0;

        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public ObservableCollection<Station> Stations { get; set; }

        public Recipe()
        {
            ID = counter++;
            Stations = new ObservableCollection<Station>();
        }
    }
}
