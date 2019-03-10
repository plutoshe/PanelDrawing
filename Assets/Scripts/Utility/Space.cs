using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceUtility
{
    public static void GetBorder(Transform obj, Camera camera, out float xMin, out float xMax, out float yMin, out float yMax)
    {
        RectTransform rect = obj.GetComponent<RectTransform>();
        Vector3 rectPos = camera.WorldToScreenPoint(obj.transform.position);
        xMin = Mathf.Round(rectPos.x - rect.pivot.x * rect.rect.width);
        xMax = Mathf.Round(rectPos.x + (1 - rect.pivot.x) * rect.rect.width);
        yMin = Mathf.Round(rectPos.y - rect.pivot.y * rect.rect.height);
        yMax = Mathf.Round(rectPos.y + (1 - rect.pivot.y) * rect.rect.height);
    }

    public static void GetBorder(Transform obj, out float xMin, out float xMax, out float yMin, out float yMax)
    {
        RectTransform rect = obj.GetComponent<RectTransform>();
        Vector3 rectPos = obj.transform.position;
        xMin = Mathf.Round(rectPos.x - rect.pivot.x * rect.rect.width);
        xMax = Mathf.Round(rectPos.x + (1 - rect.pivot.x) * rect.rect.width);
        yMin = Mathf.Round(rectPos.y - rect.pivot.y * rect.rect.height);
        yMax = Mathf.Round(rectPos.y + (1 - rect.pivot.y) * rect.rect.height);
    }


    public static void GetMinXMinY(Transform obj, Camera camera, out Vector2 topleft)
    {
        float xMin, yMin, xMax, yMax;
        GetBorder(obj, Camera.main, out xMin, out xMax, out yMin, out yMax);
        topleft.x = xMin;
        topleft.y = yMin;
    }

    public static void GetMinXMinY(Transform obj, out Vector2 topleft)
    {
        float xMin, yMin, xMax, yMax;
        GetBorder(obj, Camera.main, out xMin, out xMax, out yMin, out yMax);
        topleft.x = xMin;
        topleft.y = yMin;
    }

    public static Vector2 GetMinXMinY(Transform obj)
    {
        float xMin, yMin, xMax, yMax;
        GetBorder(obj, out xMin, out xMax, out yMin, out yMax);
        return new Vector2(xMin, yMin);
    }

    public static Vector2 GetMinXMinY(Transform obj, Camera camera)
    {
        float xMin, yMin, xMax, yMax;
        GetBorder(obj, Camera.main, out xMin, out xMax, out yMin, out yMax);
        return new Vector2(xMin, yMin);
    }

    public static void GetDistance(Transform obj1, Transform axis, Camera camera, out Vector2 dis)
    {
        float xMin, yMin, xMax, yMax;
        GetBorder(obj1, Camera.main, out xMin, out xMax, out yMin, out yMax);
        float pxMin, pyMin, pxMax, pyMax;
        GetBorder(axis, Camera.main, out pxMin, out pxMax, out pyMin, out pyMax);
        dis = new Vector2(xMin - pxMin, yMin - pyMin);
    }

    
}