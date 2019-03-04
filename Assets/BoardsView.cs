using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardsView : MonoBehaviour {
    public GameObject BoardPrefab;
    public Vector2 PanelSize;

    private IEnumerator UpdateBoard(Transform panel, PatternCollection coll)
    {
        yield return new WaitForEndOfFrame();
        PatternCollectionManager.Instance.DrawCollectionOnPanel(
                panel, coll, false);
    }
    // Use this for initialization
    void Start () {
        for (int i = 0; i < BoardManager.Instance.Boards.Count; i++)
        {
            print(PatternCollectionManager.Instance.patternCollections[i].patterns.Count);
            var currentColl = Instantiate(BoardPrefab);
            currentColl.GetComponent<DrawingBoard>().boardID = i;
            currentColl.transform.SetParent(transform, true);
            var rt = currentColl.GetComponent<RectTransform>();
            rt.sizeDelta = PanelSize;
            currentColl.SetActive(true);
            StartCoroutine(UpdateBoard(currentColl.transform, PatternCollectionManager.Instance.patternCollections[i]));
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
