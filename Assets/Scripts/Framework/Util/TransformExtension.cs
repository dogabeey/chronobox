using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtension 
{
    // Function to remove all children from a transform
    public static void Clear(this Transform transform)
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
