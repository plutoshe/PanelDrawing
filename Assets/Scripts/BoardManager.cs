using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
[Serializable]
public class Board
{
    public PatternCollection coll;
}

public class BoardManager : MonoBehaviour {
    public List<Board> Boards;
    public int currentBoardID = -1;
    public int tempOpen = 0;
    public int tempOpen1 = 0;
    private static BoardManager _instance;
    public static BoardManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new BoardManager();
            return _instance;
        }
    }

    public void DrawCollectionOnBoard(Transform panel, PatternCollection collection, bool editable)
    {
        while (panel.childCount > 0) Destroy(panel.GetChild(0).gameObject);
        foreach (var kv in collection.patterns)
        {
            print(kv.Value.localPos);
            print(kv.Value.originSize);
            print(((RectTransform)panel).rect.size);

            var xRatio = ((RectTransform)panel).rect.size.x / kv.Value.originSize.x;
            var yRatio = ((RectTransform)panel).rect.size.y / kv.Value.originSize.y;

            var newPattern = Instantiate(PatternCollectionManager.Instance.PatternPrefab);// kv.Value
            //newPattern.transform.SetParent(panel, true);
            var posOnPanel = new Vector3(
                xRatio * kv.Value.localPos.x,
                yRatio * kv.Value.localPos.y,
                kv.Value.localPos.z);
            print("Pattern Creation~~~~~~");
            print(kv.Value.localPos);
            print(posOnPanel);
            //print(Camera.main.WorldToViewportPoint(panel.position));



            newPattern.transform.position =
                Camera.main.ViewportToWorldPoint(
                posOnPanel +
                Camera.main.WorldToViewportPoint(panel.position));
            newPattern.GetComponent<PatternItemInCollection>().Set(kv.Value);
            print(newPattern.transform.position);
            newPattern.SetActive(true);


            newPattern.GetComponent<Image>().sprite =
                PatternCollectionManager.Instance.Patterns[kv.Value.PatternId].DisplayImage;

            var rt = newPattern.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(
                   rt.sizeDelta.x * xRatio,
                   rt.sizeDelta.y * yRatio);
            print("size:" + rt.sizeDelta);
            newPattern.GetComponent<PatternItemInCollection>().IsEditing = editable;
        }
    }


    BoardManager()
    {
        Boards = new List<Board>();
    }
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else if (_instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SelectionForCurrentBoard(DrawingBoard b)
    {
        currentBoardID = b.boardID;
    }

    public Board GetCurrentBoard()
    {
        if (currentBoardID >= 0 && currentBoardID < Boards.Count)
            return Boards[currentBoardID];
        else
            return null;
    }

    public void CreateBoard(PatternCollection p)
    {
        var newBoard = new Board();
        newBoard.coll = new PatternCollection();
        newBoard.coll.Set(p);
        Boards.Add(newBoard);
        currentBoardID = Boards.Count - 1;
    }
}
