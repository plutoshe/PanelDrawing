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
        var tex = new Texture2D(650, 1234);

        tex.LoadImage(fileData);
        
        //Sprite metaData = GetComponent<Image>().sprite;
        //var mainTexture = GetComponent<Image>().mainTexture;
        //Texture2D myImage = new Texture2D(mainTexture.width, mainTexture.height, TextureFormat.RGBA32, false);
        //var myImage = new Texture2D((int)metaData.rect.width, (int)metaData.rect.height);
        //var pixels = metaData.texture.GetPixels((int)metaData.textureRect.x,
        //                                        (int)metaData.textureRect.y,
        //                                        (int)metaData.textureRect.width,
        //                                        (int)metaData.textureRect.height);
        //myImage.SetPixels(pixels);
        //myImage.Apply();


        var shader1 = Shader.Find("ImageEffect/MaskIcon");
        print(GetComponent<Image>().material.name);
        GetComponent<Image>().material.shader = shader1;
       
        int m_width = (int)((RectTransform)transform).rect.width;
        int m_height = (int)((RectTransform)transform).rect.height;
        Texture2D myTexture2D = new Texture2D(m_width, m_height);
        print(m_width);
        print(m_height);
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
        AssetDatabase.Refresh();

        fileData = File.ReadAllBytes(
            Application.dataPath + "/233.PNG");
        tex = new Texture2D(80, 80);

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
