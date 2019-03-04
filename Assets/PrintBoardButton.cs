using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PrintBoardButton : MonoBehaviour
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
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {

    }
}
