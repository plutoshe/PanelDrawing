using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

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
//#if UNITY_EDITOR
        //		temp = "/StreamingAssets/ScreenShot/";
        //var temp = "/ScreenShot/";
        //string path = string.Format("{0:D4}{1:D2}.png", temp, shot_Number.ToString());
        //		filename = Application.dataPath + path;
        var filename = Application.persistentDataPath + "/1.jpg";
//#else
 
 
//#if UNITY_ANDROID
//		                temp = "/ScreenShot/";
//		                string path  = string.Format ("{0:D4}{1:D2}.png", temp,shot_Number.ToString());
//		                filename = Application.persistentDataPath + path;
 
//#endif
 
//#if UNITY_IPHONE
//		                temp = "/Raw/ScreenShot/";
//		                string path  = string.Format ("{0:D4}{1:D2}.png", temp,shot_Number.ToString());
//		                filename = Application.temporaryCachePath + path;
 
//#endif
 
//#if UNITY_STANDALONE_WIN
//		                filename = "Screenshot.png";
//#endif
//#endif
        print(filename);
        FileUtility.SaveFile(filename, Camera.main, transform.FindDeepChild("DyingPanel").gameObject);
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
