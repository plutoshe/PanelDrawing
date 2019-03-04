using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public static class WaitFor
{
   public static IEnumerator Frames(int frameCount)
    {
        while (frameCount > 0)
        {
            frameCount--;
            yield return null;
        }
    }
}


public class Combine : MonoBehaviour
{
    string resultFile = "C:\\Users\\u1209558\\Documents\\2nd_semester\\SeriousGame\\PanelDrawing\\Assets\\result.jpg";

    bool judge(Color color)
    {
        return color.r < 0.95 || color.g < 0.95 || color.b <0.95;
    }
    Color blend(Color fg, Color bg)
    {
        var r = new Color();
        r.a = 1 - (1 - 0.5f) * (1 - 0.5f);

        r.r = fg.r * 0.5f / r.a + bg.r * 0.5f * 0.5f / r.a;
        r.g = fg.g * 0.5f / r.a + bg.g * 0.5f * 0.5f / r.a;
        r.b = fg.b * 0.5f / r.a + bg.b * 0.5f * 0.5f / r.a;
        return r;
    }
    private IEnumerator AdjustTransInTheEndOfFrame()
    {
        yield return WaitFor.Frames(1);
        //yield return new WaitForEndOfFrame();
        print("!!!!");
        var tempFile = "C:\\Users\\u1209558\\Documents\\2nd_semester\\SeriousGame\\PanelDrawing\\Assets\\gg.jpg";
        Utility.SaveFile(tempFile, GameObject.Find("Main Camera").GetComponent<Camera>(), gameObject);
        var fileData = File.ReadAllBytes(
            tempFile);
        //"/Textures/Background/BGForPreface.png");

        var tex2 = new Texture2D(650, 1134);
        tex2.LoadImage(fileData);
        var tex1 = new Texture2D(650, 1134);
        bool loadSuccess = true;
        try
        {
            fileData = File.ReadAllBytes(resultFile);
            tex1.LoadImage(fileData);
        }
        catch
        {
            loadSuccess = false;
        }
        print(loadSuccess);
        int sum = 0;
        float rm = 0,gm = 0,bm = 0;
        Color transparent = new Color(1, 1, 1, 0);
        for (int x = 0; x < 650; x++)
        {
            for (int y = 0; y < 1134; y++)
            {
                float ta = tex2.GetPixel(x, y).a;
                if (loadSuccess)
                {
                    if (judge(tex2.GetPixel(x, y)))
                    {
                        

                        tex1.SetPixel(x, y, blend(tex1.GetPixel(x, y), tex2.GetPixel(x, y)));
                    }
                }
                else
                {
                    //if (tex2.GetPixel(x, y) != Color.black)
                    //{
                    //    sum++;
                    //    if (tex2.GetPixel(x, y).r > rm)
                    //        rm = tex2.GetPixel(x, y).r;
                    //    if (tex2.GetPixel(x, y).g > gm)
                    //        gm = tex2.GetPixel(x, y).g;
                    //    if (tex2.GetPixel(x, y).b > bm)
                    //        bm = tex2.GetPixel(x, y).b;
                    //    tex1.SetPixel(x, y, tex2.GetPixel(x, y));
                    //} else
                    //{
                    //    tex1.SetPixel(x, y, Color.white);
                    //}
                    if (judge(tex2.GetPixel(x, y)))
                    {
                        sum++;
                        tex1.SetPixel(x, y, ta * tex2.GetPixel(x, y));
                    }
                    else
                        tex1.SetPixel(x, y, transparent);
                }
            }
        }
        print(sum);
        print(rm + "," +gm +","+ bm);
        print(tex2.GetPixel(110, 200));
        byte[] bytes = tex1.EncodeToJPG();
        File.WriteAllBytes(resultFile, bytes);
        //obj.SetObjectActive(true);
        print(transform.childCount);
        Destroy(transform.GetChild(0).gameObject);
        print(transform.childCount);
        //while (transform.childCount > 0) Destroy(transform.GetChild(0).gameObject);
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(false);
        GetComponent<Image>().sprite = Sprite.Create(tex1, new Rect(0, 0, tex1.width, tex1.height), new Vector2(0.5f, 0.5f));
    }

    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(AdjustTransInTheEndOfFrame());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            var tempFile = "C:\\Users\\u1209558\\Documents\\2nd_semester\\SeriousGame\\PanelDrawing\\Assets\\gg1.jpg";
            Utility.SaveFile(tempFile, GameObject.Find("Main Camera").GetComponent<Camera>(), gameObject);
        }
    }
}
