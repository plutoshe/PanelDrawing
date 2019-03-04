﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawInBoard : MonoBehaviour
{
    Color currentColor;
    // Use this for initialization
    private GameObject clone;
    private LineRenderer line;
    private int i;
    public GameObject tf;
    float DyingPanelMiddleX;
    public float xMin, xMax, yMin, yMax;

    void Start()
    {
        currentColor = Color.red;
        currentColor.a = 1f;
        i = 0;

        Vector3 rectPos = Camera.main.WorldToScreenPoint(transform.position);
        RectTransform rect = GetComponent<RectTransform>();
        xMin = rectPos.x - rect.pivot.x * rect.rect.width;
        xMax = rectPos.x + (1 - rect.pivot.x) * rect.rect.width;
        yMin = rectPos.y - rect.pivot.y * rect.rect.height;
        yMax = rectPos.y + (1 - rect.pivot.y) * rect.rect.height;
        DyingPanelMiddleX = (xMin + xMax) / 2;

    }
    public static Rect RectTransformToScreenSpace(RectTransform transform)
    {
        Vector2 size = Vector2.Scale(transform.rect.size, transform.lossyScale);
        return new Rect((Vector2)transform.position - (size * 0.5f), size);
    }
    private void OnGUI()
    {
        //print("Event mous" + Event.current.mousePosition);
        //Rect rt = GUILayoutUtility.GetRect(, GUIStyle.none);
    }

    bool posInDyingPanel(Vector3 pos)
    { 
        return pos.x >= xMin - 0.1 && pos.x <= xMax + 0.1 && pos.y >= yMin - 0.1 && pos.y <= yMax + 0.1;

    }

    // Update is called once per frame
    void Update()
    {
        var currentPos = Input.mousePosition;
        if (posInDyingPanel(currentPos))
        {
            if (Input.GetMouseButtonDown(0))
            {

                clone = Instantiate(tf, tf.transform.position, transform.rotation);//克隆一个带有LineRender的物体
                line = clone.GetComponent<LineRenderer>();//获得该物体上的LineRender组件
                line.material.color = currentColor;
                currentColor.a = 0.5f;
                line.startColor = currentColor;
                line.endColor = currentColor;//new Vector4(0.3f, 0.4f, 0.6f, 0.3f);
                line.startWidth = 8.45f;
                line.endWidth = 8.45f;
                clone.SetActive(true);
                i = 0;
            }
            if (Input.GetMouseButton(0))
            {
                var conversion = Camera.main.ScreenToWorldPoint(new Vector3(
                        DyingPanelMiddleX,
                        currentPos.y, 15));

                if (i < 1 || line.GetPosition(i - 1).y != conversion.y)
                {
                    i++;
                    //line.SetVertexCount(i);//设置顶点数
                    line.positionCount = i;

                    line.SetPosition(i - 1, conversion);
                }
                                                                                                                               //line.enabled=false;
            }
        }

    }

    public void ChangeColor(Color updateColor)
    {
        currentColor = updateColor;
    }
}
