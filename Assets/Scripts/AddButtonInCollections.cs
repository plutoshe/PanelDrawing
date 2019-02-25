using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddButtonInCollections : MonoBehaviour {
    public string gotoScene;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Click()
    {
        PatternCollectionManager.Instance.NewCollection();
        SceneController.Instance.LoadScene(gotoScene);
    }
}
