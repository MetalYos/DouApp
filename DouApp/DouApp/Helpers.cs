using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using DouApp.Models;

namespace DouApp
{
    public static class Helpers
    {
        public static void SortUserRecipes(this ObservableCollection<UserRecipe> collection, Comparison<UserRecipe> comparison = null)
        {
            var sortableList = new List<UserRecipe>(collection);
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
