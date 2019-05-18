using System;
using System.Collections.Generic;
using System.Text;

namespace DouApp.Models
{
    public class Station
    {
        // Remove once we are using SQL databases
        private static int counter = 0;

        public int ID { get; set; }
        public Container Container { get; set; }
        public double Weight { get; set; }
        public int Spoon { get; set; }

        public Station()
        {
            ID = counter++;
        }

        public void SetLargeContainer(Container container, double weight)
        {
            Container = container;
            Weight = weight;
            Spoon = 0;
        }

        public void SetSmallContainer(Container container, int spoon)
        {
            Container = container;
            Spoon = spoon;
            Weight = 0;
        }
    }
}
