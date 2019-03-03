using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternCollectionManager : MonoBehaviour {
    static PatternCollectionManager _instance;
    public static PatternCollectionManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new PatternCollectionManager();
            return _instance;
        }
    }

    PatternCollectionManager()
    {
        patternCollections = new List<PatternCollection>();
    }

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else if (_instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        
    }
    public List<PatternAttr> Patterns;
    public List<PatternCollection> patternCollections;
    public PatternCollection currentCollection;

    // Use this for initialization
    void Start () {
        print("pattern manager start");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.U))
        {
            print(currentCollection.patterns.Count);
        }
	}

    public void AddToCurrentCollection(GameObject pattern)
    {
        if (currentCollection != null)
        {
            currentCollection.patterns.Add(pattern.GetInstanceID(), pattern.GetComponent<PatternItemInCollection>());
        }
    }

    public void UpdatePatternPosition(GameObject pattern, Vector3 localPos)
    {
        //currentCollection.patterns[pattern.GetInstanceID()].localPos = localPos;
    }

    public void RemoveFromCurrentCollection(GameObject pattern)
    {
        if (currentCollection != null)
        {
            currentCollection.patterns.Remove(pattern.GetInstanceID());
        }
    }

    public void NewCollection()
    {
        print("newCollection");

        currentCollection = new PatternCollection();
        print(currentCollection.patterns.Count);
       
        patternCollections.Add(currentCollection);
        print(currentCollection.patterns.Count);
    }
}
