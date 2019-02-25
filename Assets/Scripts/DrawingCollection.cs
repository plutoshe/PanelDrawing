using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DrawingCollection : MonoBehaviour {
    public PatternCollection collection;
    public GameObject PatternPrefab;
	// Use this for initialization
    public void RefreshUI()
    {
        while (transform.childCount > 0) Destroy(transform.GetChild(0).gameObject);
        foreach (var kv in collection.patterns)
        {
            print(kv.Value.localPos);
            print(kv.Value.originSize);
            print(((RectTransform)transform).rect.size);
            var newPattern = Instantiate(PatternPrefab);// kv.Value
            newPattern.transform.SetParent(transform, true);
            newPattern.transform.position =
                Camera.main.ScreenToWorldPoint(
                kv.Value.localPos +
                Camera.main.WorldToScreenPoint(transform.position));
            newPattern.GetComponent<PatternItemInCollection>().Set(kv.Value);
            print(newPattern.transform.localPosition);
            newPattern.SetActive(true);
            newPattern.GetComponent<Image>().sprite = PatternCollectionManager.Instance.Patterns[kv.Value.PatternId].DisplayImage;
        }
    }

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
