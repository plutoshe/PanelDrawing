using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector3Extension {
    public static Vector3 ConvertFromVector2(this Vector3 a, Vector2 inVector)
    {
        return new Vector3(inVector.x, inVector.y);
    }
}

public class VectorUtility
{
    public static Vector3 ConvertFromVector2(Vector2 inVector)
    {
        return new Vector3(inVector.x, inVector.y);
    }
}