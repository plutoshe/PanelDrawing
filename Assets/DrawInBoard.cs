using System.Collections;
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
    void Start()
    {
        currentColor = Color.red;
        currentColor.a = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetMouseButtonDown(0))
        {

            clone = (GameObject)Instantiate(tf, tf.transform.position, transform.rotation);//克隆一个带有LineRender的物体
            //clone.gameObject.GetComponent<LineRendersTest>().enabled=false;
            //clone.GetComponent<LineRenderer>().enabled=true;
            line = clone.GetComponent<LineRenderer>();//获得该物体上的LineRender组件
            //line.material.color = currentColor;
            //line.SetColors(Color.blue, Color.red);//设置颜色
            //var cc = new Color(0f, 0f, 0f, 0.1f);
            //cc.a = 0.1f;
            //line.material.color = cc;
            //line.material.color = new Color(1f, 0.4f, 0.6f, 0.3f);
            line.material.color = currentColor;
            //line.material.color.a = 0.3f;
            //line.material = new Material(Shader.Find("Particles/Additive"));
            //Color.red;
            //line.material.SetColor("_TintColor", new Color(0.3f, 0.3f, 0.3f, 0.5f));
            print(currentColor);
            //line.material.SetFloat("_Alpha", 0.1f);
            currentColor.a = 0.5f;
            line.startColor = currentColor;
            //line.colorGradient.alphaKeys.alp
            //new Vector4(0.3f, 0.4f, 0.6f, 0.3f);
            line.endColor = currentColor;//new Vector4(0.3f, 0.4f, 0.6f, 0.3f);
            //line.SetWidth(0.2f, 0.1f);//设置宽度
            //line.SetWidth(3, 3);
            line.startWidth = 1;
            line.endWidth = 1;
            clone.SetActive(true);
            i = 0;
            //float alpha = 1.0f;
            //Gradient gradient = new Gradient();
            ////currentColor.a = 1f;
            //gradient.SetKeys(
            //    new GradientColorKey[] { new GradientColorKey(currentColor, 0.0f), new GradientColorKey(currentColor, 1.0f) },
            //    new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.1f), new GradientAlphaKey(alpha, 0.1f) }
            //);
            //line.colorGradient = gradient;

        }
        if (Input.GetMouseButton(0))
        {

            i++;
            line.SetVertexCount(i);//设置顶点数
            line.SetPosition(i - 1, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 15)));//设置顶点位置
            //line.enabled=false;
        }

    }

    public void ChangeColor(Color updateColor)
    {
        currentColor = updateColor;
    }
}
