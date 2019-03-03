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
    
    public Transform PatternDisplayPanel;
    public Transform PatternPaintingPanel;
    public Transform PatternSamplePanel;
    public Transform DeleteButton;
    public Transform FinishEditingButton;
    public Transform selectedPatternItem;

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
        for (int i = 0; i < PatternCollectionManager.Instance.Patterns.Count; i++)
        {
            var newPattern = Instantiate(PatternItemPrefab);
            newPattern.GetComponent<PatternItem>().attr = PatternCollectionManager.Instance.Patterns[i];
            newPattern.GetComponent<PatternItem>().attr.PatternId = i;

            newPattern.transform.SetParent(PatternDisplayPanel, true);
            newPattern.SetActive(true);

            var newPatternSample = Instantiate(PatternItemInCollectionPrefab);
            newPatternSample.GetComponent<Image>().sprite = PatternCollectionManager.Instance.Patterns[i].DisplayImage;
            newPatternSample.GetComponent<PatternItemInCollection>().Archetype = newPattern;
            newPatternSample.transform.SetParent(PatternSamplePanel, true);
            
            StartCoroutine(AdjustTransInTheEndOfFrame(newPattern, newPatternSample));

        }
        DeleteButton.GetComponent<Button>().onClick.AddListener(DeleteSelectedItemAction);
        FinishEditingButton.GetComponent<Button>().onClick.AddListener(FinishEditingAction);
    }

    public bool OnEditingPattern()
    {
        return !(selectedPatternItem == null);
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
        return RectIntercept(PatternPaintingPanel, patternDrawable.transform);
    }

    //bool InDeletion(GameObject patternDrawable)
    //{
    //    return RectIntercept(DeleteButton, patternDrawable.transform);
    //}

    public void DrawPaintingPanel(GameObject patternDrawable)
    {
        if (InPaintingPanel(patternDrawable))
        {

            var newPattern = Instantiate(PatternItemInCollectionPrefab);
            newPattern.transform.position = patternDrawable.transform.position;
            newPattern.GetComponent<Image>().sprite = patternDrawable.GetComponent<Image>().sprite;
            newPattern.GetComponent<PatternItemInCollection>().IsAchetype = false;
            newPattern.GetComponent<PatternItemInCollection>().originSize = ((RectTransform)PatternPaintingPanel.transform).rect.size;
            newPattern.GetComponent<PatternItemInCollection>().localPos = Camera.main.WorldToScreenPoint(newPattern.transform.position) - Camera.main.WorldToScreenPoint(PatternPaintingPanel.position); //newPattern.transform.localPosition;
            newPattern.GetComponent<PatternItemInCollection>().PatternId = patternDrawable.GetComponent<PatternItemInCollection>().Archetype.GetComponent<PatternItem>().attr.PatternId;
            newPattern.transform.SetParent(PatternPaintingPanel, true);
            newPattern.name = "Drawing";
            newPattern.SetActive(true);
            newPattern.GetComponent<PatternItemInCollection>().ToggleSelectItem();
            PatternCollectionManager.Instance.AddToCurrentCollection(newPattern);
        }
    }

    public void CheckDeletion(GameObject patternDrawable)
    {
        //print(InDeletion(patternDrawable));
        //if (InDeletion(patternDrawable) && !patternDrawable.GetComponent<PatternItemInCollection>().IsAchetype)
        //{
        //    PatternCollectionManager.Instance.RemoveFromCurrentCollection(patternDrawable);
        //    Destroy(patternDrawable);
        //}
    }

    public void DeleteSelectedItemAction()
    {
        Destroy(selectedPatternItem.gameObject);
    }

    public void FinishEditingAction()
    {
        selectedPatternItem.transform.GetComponent<PatternItemInCollection>().OnUnSelection();
        selectedPatternItem = null;
    }

    // Update is called once per frame
    void Update () {
        DeleteButton.gameObject.SetActive(OnEditingPattern());
        FinishEditingButton.gameObject.SetActive(OnEditingPattern());
        if (OnEditingPattern()) { 
            GestureOperation();
            if (Input.GetKey(KeyCode.U))
            {
                selectedPatternItem.Rotate(Vector3.forward * 3, Space.World);
                var currentPosition = Camera.main.WorldToViewportPoint(selectedPatternItem.position);
                var currentxyPostion = new Vector2(currentPosition.x, currentPosition.y);
                print(currentxyPostion);
            }
        }
    }
    private Touch oldTouch1;  //上次触摸点1(手指1)  
    private Touch oldTouch2;  //上次触摸点2(手指2)  

    void GestureOperation()
    {
        if (selectedPatternItem == null)
            return;

        if (Input.touchCount <= 0)
        {
            return;
        }

        Touch newTouch1 = Input.GetTouch(0);

        if (1 == Input.touchCount)
        {
            if (newTouch1.phase == TouchPhase.Began)
            {
                oldTouch1 = newTouch1;
                return;
            }
            Touch touch = Input.GetTouch(0);
            var currentPosition = Camera.main.WorldToViewportPoint(selectedPatternItem.position);
            var currentxyPostion = new Vector2(currentPosition.x, currentPosition.y);
            float distance = Vector2.SignedAngle(
                touch.position - currentxyPostion, 
                oldTouch1.position - currentxyPostion);
            selectedPatternItem.Rotate(Vector3.forward * distance * 0.08f, Space.World);
        }

        //多点触摸, 放大缩小  
        Touch newTouch2 = Input.GetTouch(1);

        //第2点刚开始接触屏幕, 只记录，不做处理  
        if (newTouch2.phase == TouchPhase.Began)
        {
            oldTouch2 = newTouch2;
            oldTouch1 = newTouch1;
            return;
        }

        //计算老的两点距离和新的两点间距离，变大要放大模型，变小要缩放模型  
        float oldDistance = Vector2.Distance(oldTouch1.position, oldTouch2.position);
        float newDistance = Vector2.Distance(newTouch1.position, newTouch2.position);

        //两个距离之差，为正表示放大手势， 为负表示缩小手势  
        float offset = newDistance - oldDistance;

        //放大系数
        float scaleFactor = offset / 100f;
        Vector3 localScale = selectedPatternItem.localScale;
        float minS = 0.1f, maxS = 10f;
        Vector3 scale = new Vector3(Mathf.Clamp(localScale.x + scaleFactor, minS, maxS),
                                    Mathf.Clamp(localScale.y + scaleFactor, minS, maxS),
                                    Mathf.Clamp(localScale.z + scaleFactor, minS, maxS));

        //最小缩放到 0.1 倍  
        if (scale.x > 0.1f && scale.y > 0.1f && scale.z > 0.1f)
        {
            selectedPatternItem.localScale = scale;
        }
        selectedPatternItem.GetComponent<PatternItemInCollection>().UpdateUIForChange();



    }

    public void GeneratePatternInCollection(PatternAttr attr)
    {
        
    }
}
