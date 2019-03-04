using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class Board
{
    PatternCollection coll;
}

public class BoardManager : MonoBehaviour {
    public List<Board> Boards;
    public int currentBoardID = -1;
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
}
