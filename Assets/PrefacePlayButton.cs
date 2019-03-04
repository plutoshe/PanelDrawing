using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrefacePlayButton : MonoBehaviour
{
    public string gotoScene;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        SceneController.Instance.LoadScene(gotoScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
