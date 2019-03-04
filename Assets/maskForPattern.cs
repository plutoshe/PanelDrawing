using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

using UnityEditor;

public class maskForPattern : MonoBehaviour {
    public string textureName;
    public Material sample;
    public Texture2D pattern;
    public int startY = 0, startX = 0;
    // Use this for initialization
    void Start () {

        var fileData = File.ReadAllBytes(
            Application.dataPath + "/1.jpg");
        //"/Textures/Background/BGForPreface.png");
        var tex = new Texture2D(650, 1134);

        tex.LoadImage(fileData);
        var shader1 = Shader.Find("ImageEffect/MaskIcon");
        GetComponent<Image>().material.shader = shader1;

        Camera camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        Vector3 rectPos = camera.WorldToScreenPoint(transform.position);
        RectTransform rect = GetComponent<RectTransform>();
        var xMin = rectPos.x - rect.pivot.x * rect.rect.width;
        var yMin = rectPos.y - rect.pivot.y * rect.rect.height;

        rectPos = camera.WorldToScreenPoint(transform.parent.position);
        rect = GetComponent<RectTransform>();
        var pxMin = rectPos.x - rect.pivot.x * rect.rect.width;
        var pyMin = rectPos.y - rect.pivot.y * rect.rect.height;
        startX = (int)(xMin - pxMin);
        startY = (int)(yMin - pyMin);
        int m_width = (int)GetComponent<RectTransform>().rect.width;
        int m_height = (int)GetComponent<RectTransform>().rect.height;
        Texture2D myTexture2D = new Texture2D(m_width, m_height);

        for (int y = startY; y < startY + m_height; y++)//Y轴像素
        {
            for (int x = startX; x < startX + m_width; x++)
            {
                myTexture2D.SetPixel(x, y, tex.GetPixel(x, y));
               // print(tex.GetPixel(x, y));
            }
        }


        //for (int y = (int)metaData.rect.y; y < metaData.rect.y + m_height; y++)//Y轴像素
        //{
        //    for (int x = (int)metaData.rect.x; x < metaData.rect.x + m_width; x++)
        //    {
        //        myTexture2D.SetPixel(x - (int)metaData.rect.x, y - (int)metaData.rect.y, myImage.GetPixel(x, y));
        //        print(myImage.GetPixel(x, y));
        //    }
        //}
        myTexture2D.Apply();
        if (myTexture2D.format != TextureFormat.ARGB32 && myTexture2D.format != TextureFormat.RGB24)
        {
            Texture2D newTexture = new Texture2D(myTexture2D.width, myTexture2D.height);
            newTexture.SetPixels(myTexture2D.GetPixels(0), 0);
            myTexture2D = newTexture;

           
        }
        var pngData = myTexture2D.EncodeToPNG();


        //AssetDatabase.CreateAsset(myimage, rootPath + "/" + image.name + "/" + metaData.name + ".PNG");
        File.WriteAllBytes(Application.dataPath + "/233.PNG", pngData);
#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif

        fileData = File.ReadAllBytes(
            Application.dataPath + "/233.PNG");
        tex = new Texture2D(m_width, m_height);

        tex.LoadImage(fileData);
        //MaterialPropertyBlock prop = new MaterialPropertyBlock();
        Image imageComp = GetComponent<Image>();
        imageComp.material = Instantiate(sample);

        //myTexture2D.ReadPixels(new Rect(100, 100, renderTexture.width - 100, renderTexture.height - 100), 0, 0);
        //GetComponent<Image>().sprite = Sprite.Create(myTexture2D, 
        //    new Rect(0.0f, 0.0f, myTexture2D.width, myTexture2D.height), 
        //    new Vector2(0.5f, 0.5f), 100.0f);



        imageComp.material.SetTexture("_MainTex", tex);


       
        imageComp.material.SetTexture("_Mask", pattern);
        
        //GetComponent<Image>().material.SetPropertyBlock(prop);
        //GetComponent<Image>().material.SetFloat("_Stencil", 10);
    }
	
	// Update is called once per frame
	void Update () {
        
        
    }
}
