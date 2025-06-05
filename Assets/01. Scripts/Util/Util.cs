using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    public static T FindChild<T>(this Transform transform, string name) where T : Component
    {
        T[] temp = transform.GetComponentsInChildren<T>(true);
        foreach (T child in temp)
        {
            if (child.name == name)
            {
                return child;
            }
        }
        return null;
    }
}
