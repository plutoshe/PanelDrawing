using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public int currentCollectionID;
    public GameObject PatternPrefab;
    

    // Use this for initialization
    void Start () {
        print("pattern manager start");
        currentCollectionID = -1;

    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.U) && currentCollectionID >= 0 && currentCollectionID < patternCollections.Count)
        {
            print(patternCollections[currentCollectionID].patterns.Count);
        }
	}

    public void AddToCurrentCollection(GameObject pattern)
    {
        if (currentCollectionID >= 0 && currentCollectionID < patternCollections.Count)
        {
            patternCollections[currentCollectionID].patterns.Add(pattern.GetInstanceID(), pattern.GetComponent<PatternItemInCollection>());
        }
    }


    public void RemoveFromCurrentCollection(GameObject pattern)
    {
        if (currentCollectionID >= 0 && currentCollectionID < patternCollections.Count)
        {
            patternCollections[currentCollectionID].patterns.Remove(pattern.GetInstanceID());
        }
    }

    public void UpdateCurrentCollection(GameObject pattern)
    {
        //if (currentCollection != null)
        //{
        //    currentCollection.patterns[pattern.GetInstanceID()].localPos = ;
        //}
    }

    public void NewCollection()
    {
        print("newCollection");

        patternCollections.Add(new PatternCollection());
        currentCollectionID = patternCollections.Count - 1;
    }

    public PatternCollection GetCurrentCollection()
    {
        if (currentCollectionID >= 0 && currentCollectionID < patternCollections.Count)
            return patternCollections[currentCollectionID];
        else
            return null;
     }

    public void DrawCollectionOnPanel(Transform panel, PatternCollection collection, bool editable)
    {
        while (panel.childCount > 0) Destroy(panel.GetChild(0).gameObject);
        foreach (var kv in collection.patterns)
        {
            print(kv.Value.localPos);
            print(kv.Value.originSize);
            print(((RectTransform)panel).rect.size);

            var xRatio = ((RectTransform)panel).rect.size.x / kv.Value.originSize.x;
            var yRatio = ((RectTransform)panel).rect.size.y / kv.Value.originSize.y;

            var newPattern = Instantiate(PatternPrefab);// kv.Value
            newPattern.transform.SetParent(panel, true);
            var posOnPanel = new Vector3(
                xRatio * kv.Value.localPos.x,
                yRatio * kv.Value.localPos.y,
                kv.Value.localPos.z);
            print("Pattern Creation~~~~~~");
            print(kv.Value.localPos);
            print(posOnPanel);

            newPattern.transform.localScale = kv.Value.scale;
            newPattern.transform.rotation = kv.Value.rotation;
            
            newPattern.transform.position =
                posOnPanel + VectorUtility.ConvertFromVector2(SpaceUtility.GetMinXMinY(panel));
            newPattern.GetComponent<PatternItemInCollection>().Set(kv.Value);
            print(newPattern.transform.position);
            newPattern.SetActive(true);
            

            newPattern.GetComponent<Image>().sprite =
                Patterns[kv.Value.PatternId].DisplayImage;

            var rt = newPattern.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(
                   rt.sizeDelta.x * xRatio,
                   rt.sizeDelta.y * yRatio);
            print("size:" + rt.sizeDelta);
            newPattern.GetComponent<PatternItemInCollection>().IsEditing = editable;
        }
    }


    public void SelectionForCurrentCollection(DrawingCollection selection)
    {
        currentCollectionID = selection.collectionID;
    }


    public bool isSelecting()
    {
        return currentCollectionID >= 0;
    }
}
