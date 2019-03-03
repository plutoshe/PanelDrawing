using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionsView : MonoBehaviour {
    public GameObject CollectionPrefab;
    public Vector2 PanelSize;
    // Use this for initialization

    private IEnumerator UpdateCollection(Transform panel, PatternCollection coll)
    {
        yield return new WaitForEndOfFrame();
        PatternCollectionManager.Instance.DrawCollectionOnPanel(
                panel, coll);
    }
    void Start () {
        for (int i = 0; i < PatternCollectionManager.Instance.patternCollections.Count; i++)
        {
            print(PatternCollectionManager.Instance.patternCollections[i].patterns.Count);
            var currentColl = Instantiate(CollectionPrefab);
            currentColl.GetComponent<DrawingCollection>().collection = PatternCollectionManager.Instance.patternCollections[i];
            currentColl.transform.SetParent(transform, true);
            var rt = currentColl.GetComponent<RectTransform>();
            rt.sizeDelta = PanelSize;
            currentColl.SetActive(true);
            StartCoroutine(UpdateCollection(currentColl.transform, PatternCollectionManager.Instance.patternCollections[i]));
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
