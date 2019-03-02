using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class captureImage : MonoBehaviour {

    public Camera mainCam; //待截图的目标摄像机
    RenderTexture rt;  //声明一个截图时候用的中间变量 
    Texture2D t2d;
    int num = 0;  //截图计数

    //public GameObject pl;  //一个调试用的板子



    void Start()
    {


    }
    int shot_Number;
    void SaveFile ()
	{
 
		shot_Number++;
		if (shot_Number > 9) {
			shot_Number = 0;
		}
 
		//HideUI ();
		//yield return new WaitForEndOfFrame ();
		//保存
 
		//yield return new WaitForSeconds(0.5f);
        # if UNITY_EDITOR
        //		temp = "/StreamingAssets/ScreenShot/";
		        var temp = "/ScreenShot/";
		        string path = string.Format ("{0:D4}{1:D2}.png", temp, shot_Number.ToString ());
        //		filename = Application.dataPath + path;
		        var filename = Application.dataPath + "/" + shot_Number.ToString() + ".jpg";
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
		Camera earthCamera;
		GameObject earth = GameObject.Find ("Main Camera");
		if (earth) {
			earthCamera = earth.GetComponent<Camera> ();
		} else {
			return;
		}
        Debug.Log("保存图片的路径`1" + filename);
        RenderTexture renderTexture;
		//深度问题depth
		renderTexture = new RenderTexture (Screen.width, Screen.height, 24);
		earthCamera.targetTexture = renderTexture;
		earthCamera.Render ();
 
		Texture2D myTexture2D = new Texture2D (renderTexture.width - 100, renderTexture.height -100);
		RenderTexture.active = renderTexture;
		myTexture2D.ReadPixels (new Rect (100, 100, renderTexture.width - 100, renderTexture.height - 100), 0, 0);
		myTexture2D.Apply ();
		byte[] bytes = myTexture2D.EncodeToJPG ();
		myTexture2D.Compress (true);
		myTexture2D.Apply ();  
		RenderTexture.active = null;
 
 
		System.IO.File.WriteAllBytes (filename, bytes);  
		//Debug.Log (string.Format ("截屏了一张图片: {0}", filename));  
		Debug.Log ("保存图片的路径" + filename);
 
		earthCamera.targetTexture = null;  
		GameObject.Destroy (renderTexture); 
 
	}

    void Update()
    {
        //按下空格键来截图
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SaveFile();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            SaveFile();
            SceneController.Instance.LoadScene("AfterDrawing");
        }
    }
}
