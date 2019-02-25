using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatternManager : MonoBehaviour {
    static PatternManager _instance;
    public static PatternManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new PatternManager();
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        if (_instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    
    public List<PatternAttr> Patterns;
    public Transform PatternDisplayPanel;
    public Transform PattrenPaintingPanel;
    public Transform PatternSamplePanel;
    public Transform DeleteButton;


    public GameObject PatternItemPrefab;
    public GameObject PatternItemInCollectionPrefab;
    
    private IEnumerator AdjustTransInTheEndOfFrame(GameObject obj, GameObject adjust_obj)
    {
        yield return new WaitForEndOfFrame();
        adjust_obj.transform.position = obj.transform.position;
        
        adjust_obj.SetActive(true);
        //obj.SetObjectActive(true);
    }
    // Use this for initialization
    void Start () {
        for (int i = 0; i < Patterns.Count; i++)
        {
            var newPattern = Instantiate(PatternItemPrefab);
            newPattern.GetComponent<PatternItem>().attr = Patterns[i];

            newPattern.transform.SetParent(PatternDisplayPanel, true);
            newPattern.SetActive(true);

            var newPatternSample = Instantiate(PatternItemInCollectionPrefab);
            newPatternSample.GetComponent<Image>().sprite = Patterns[i].DisplayImage;
            newPatternSample.GetComponent<PatternItemInCollection>().Archetype = newPattern;
            newPatternSample.transform.SetParent(PatternSamplePanel, true);

            StartCoroutine(AdjustTransInTheEndOfFrame(newPattern, newPatternSample));

        }
    }

    bool RectIntercept(Transform a, Transform b)
    {
        Vector3[] v = new Vector3[4];
        ((RectTransform)a).GetWorldCorners(v);
        Vector2 aLeftTop = v[0];
        Vector2 aRightBottom = v[2];
        ((RectTransform)b).GetWorldCorners(v);
        Vector2 bLeftTop = v[0];
        Vector2 bRightBottom = v[2];

        if (aLeftTop.y >= bRightBottom.y)
            return false;
        if (aLeftTop.x >= bRightBottom.x)
            return false;
        if (aRightBottom.y <= bLeftTop.y)
            return false;
        if (aRightBottom.x <= bLeftTop.x)
            return false;

        return true;
    }

    bool InPaintingPanel(GameObject patternDrawable)
    {
        return RectIntercept(PattrenPaintingPanel, patternDrawable.transform);
    }

    bool InDeletion(GameObject patternDrawable)
    {
        return RectIntercept(DeleteButton, patternDrawable.transform);
    }

    public void DrawPaintingPanel(GameObject patternDrawable)
    {
        if (InPaintingPanel(patternDrawable))
        {

            var newPattern = Instantiate(PatternItemInCollectionPrefab);
            newPattern.transform.position = patternDrawable.transform.position;
            newPattern.GetComponent<Image>().sprite = patternDrawable.GetComponent<Image>().sprite;
            newPattern.GetComponent<PatternItemInCollection>().IsAchetype = false;
            newPattern.transform.SetParent(PattrenPaintingPanel, true);
            newPattern.name = "Drawing";
            newPattern.SetActive(true);
        }
    }

    public void CheckDeletion(GameObject patternDrawable)
    {
        print(InDeletion(patternDrawable));
        if (InDeletion(patternDrawable) && !patternDrawable.GetComponent<PatternItemInCollection>().IsAchetype)
        {
            Destroy(patternDrawable);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void GeneratePatternInCollection(PatternAttr attr)
    {
        
    }
}
