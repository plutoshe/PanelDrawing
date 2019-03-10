using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileUtility
{
    // Start is called before the first frame update
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
        SpaceUtility.GetBorder(target.transform, earthCamera, out xMin, out xMax, out yMin, out yMax);
        Debug.Log(xMin + "," + xMax + "," + yMin + "," + yMax);
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
