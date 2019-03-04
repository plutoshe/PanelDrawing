using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class Utility
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

    public static void SaveFile(string filename, Camera earthCamera, GameObject target)
    {
        //Debug.Log("保存图片的路径`1" + filename);
        RenderTexture renderTexture;
        //深度问题depth

        renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
        earthCamera.targetTexture = renderTexture;
        earthCamera.Render();
        var targetRect = target.GetComponent<RectTransform>();
        Texture2D myTexture2D = new Texture2D((int)targetRect.rect.width, (int)targetRect.rect.height);
        RenderTexture.active = renderTexture;
        float xMin, yMin, xMax, yMax;
        GetBorder(target.transform, earthCamera, out xMin, out xMax, out yMin, out yMax);
        Debug.Log(xMin + ","+ xMax+","+ yMin+","+ yMax);
        myTexture2D.ReadPixels(new Rect(xMin, yMin, targetRect.rect.width, targetRect.rect.height), 0, 0);
        myTexture2D.Apply();
        byte[] bytes = myTexture2D.EncodeToJPG();
        myTexture2D.Compress(true);
        myTexture2D.Apply();
        RenderTexture.active = null;


        System.IO.File.WriteAllBytes(filename, bytes);
        //Debug.Log (string.Format ("截屏了一张图片: {0}", filename));  
        //Debug.Log("保存图片的路径" + filename);

        earthCamera.targetTexture = null;

    }
}
public class captureImage : MonoBehaviour {

    public Camera mainCam; //待截图的目标摄像机
    RenderTexture rt;  //声明一个截图时候用的中间变量 
    Texture2D t2d;
    int num = 0;  //截图计数

    void Start()
    {


    }
    int shot_Number;
    

    void SaveFileInPrinting()
    {
#if UNITY_EDITOR
        //		temp = "/StreamingAssets/ScreenShot/";
        //var temp = "/ScreenShot/";
        //string path = string.Format("{0:D4}{1:D2}.png", temp, shot_Number.ToString());
        //		filename = Application.dataPath + path;
        var filename = Application.dataPath + "/1.jpg";
#else
 
 
#if UNITY_ANDROID
		                temp = "/ScreenShot/";
		                string path  = string.Format ("{0:D4}{1:D2}.png", temp,shot_Number.ToString());
		                filename = Application.persistentDataPath + path;
 
#endif
 
#if UNITY_IPHONE
		                temp = "/Raw/ScreenShot/";
		                string path  = string.Format ("{0:D4}{1:D2}.png", temp,shot_Number.ToString());
		                filename = Application.temporaryCachePath + path;
 
#endif
 
#if UNITY_STANDALONE_WIN
		                filename = "Screenshot.png";
#endif
#endif
        print(filename);
        Utility.SaveFile(filename, Camera.main, transform.Find("DyingPanel").gameObject);
    }
    

    void Update()
    {
        //按下空格键来截图
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SaveFileInPrinting();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            SaveFileInPrinting();
            SceneController.Instance.LoadScene("AfterDrawing");
        }
    }
}
