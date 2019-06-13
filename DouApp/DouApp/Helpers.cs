﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DouApp
{
    public static class Helpers
    {
        public static void Sort<T>(this ObservableCollection<T> collection, Comparison<T> comparison = null)
        {
            var sortableList = new List<T>(collection);
            if (comparison == null)
                sortableList.Sort();
            else
                sortableList.Sort(comparison);

            for (var i = 0; i < sortableList.Count; i++)
            {
                var oldIndex = collection.IndexOf(sortableList[i]);
                var newIndex = i;
                if (oldIndex != newIndex)
                    collection.Move(oldIndex, newIndex);
            }
        }
    }
}
