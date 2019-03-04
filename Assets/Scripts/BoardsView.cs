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
            var currentBoard = Instantiate(BoardPrefab);
            currentBoard.GetComponent<DrawingBoard>().boardID = i;
            currentBoard.transform.SetParent(transform, true);
            var rt = currentBoard.GetComponent<RectTransform>();
            rt.sizeDelta = PanelSize;
            currentBoard.SetActive(true);
            StartCoroutine(UpdateBoard(currentBoard.transform, PatternCollectionManager.Instance.patternCollections[i]));
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
