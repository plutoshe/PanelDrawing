using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingUpdateButton : MonoBehaviour {
    public string gotoScene;
    // Use this for initialization
    void Start () {
		
	}
	
    public void UpdateButtonAction()
    {
        
        if (PatternCollectionManager.Instance.isSelecting())
        {
            print(PatternCollectionManager.Instance.currentCollectionID);
            SceneController.Instance.LoadScene(gotoScene);
        }
    }

	// Update is called once per frame
	void Update () {
        //gameObject.SetActive(PatternCollectionManager.Instance.currentSelection != null);
    }
}
