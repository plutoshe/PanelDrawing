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
    public PatternCollection currentCollection;
    public GameObject PatternPrefab;

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


    public void RemoveFromCurrentCollection(GameObject pattern)
    {
        if (currentCollection != null)
        {
            currentCollection.patterns.Remove(pattern.GetInstanceID());
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

        currentCollection = new PatternCollection();
        print(currentCollection.patterns.Count);
       
        patternCollections.Add(currentCollection);
        print(currentCollection.patterns.Count);
    }

    public Vector3 GetTopLeftOfCanvasObject(GameObject obj)
    {
        float minX = obj.GetComponent<RectTransform>().position.x + obj.GetComponent<RectTransform>().rect.xMin;
        float maxY = obj.GetComponent<RectTransform>().position.y + obj.GetComponent<RectTransform>().rect.yMax;
        float z = obj.GetComponent<RectTransform>().position.z;

        return new Vector3(minX, maxY, z);
    }

    public void DrawCollectionOnPanel(Transform panel, PatternCollection collection)
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
            print(Camera.main.WorldToViewportPoint(panel.position));
            newPattern.transform.position =
                Camera.main.ViewportToWorldPoint(
                posOnPanel +
                Camera.main.WorldToViewportPoint(panel.position));
            //newPattern.transform.localPosition = posOnPanel;

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
        }
    }
}
