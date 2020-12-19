using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Static class which adds elements to a list
public static class ListExtenstions
{
    public static void AddMany<T>(this List<T> list, params T[] elements)
    {
        list.AddRange(elements);
    }
}
