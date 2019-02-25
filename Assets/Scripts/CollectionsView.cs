using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionsView : MonoBehaviour {
    public GameObject CollectionPrefab;
	// Use this for initialization
	void Start () {
        print("~~~~" + PatternCollectionManager.Instance.currentCollection.patterns.Count);
        for (int i = 0; i < PatternCollectionManager.Instance.patternCollections.Count; i++)
        {
            print(PatternCollectionManager.Instance.patternCollections[i].patterns.Count);
            var currentColl = Instantiate(CollectionPrefab);
            currentColl.GetComponent<DrawingCollection>().collection = PatternCollectionManager.Instance.patternCollections[i];
            currentColl.transform.SetParent(transform, true);
            currentColl.GetComponent<DrawingCollection>().RefreshUI();
            currentColl.SetActive(true);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
