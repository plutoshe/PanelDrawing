using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DrawingCollection : MonoBehaviour {
    public int collectionID;
    public GameObject PatternPrefab;
    public Material outlineMaterial;
    // Use this for initialization


    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void UpdateSelectItemUI(bool isSelected)
    {
        if (isSelected)
        {
            print(outlineMaterial.name);
            print(gameObject.name);
            GetComponent<Image>().material = Instantiate(outlineMaterial);
            GetComponent<Image>().material.SetVector("_ObjectScale", transform.localScale);
            GetComponent<Image>().material.SetVector("_ObjectRect", ((RectTransform)transform).rect.size);
            //oldSelectItem.
        }
        else
        {
            GetComponent<Image>().material = null;
        }
    }

    public void OnClick()
    {
        PatternCollectionManager.Instance.SelectionForCurrentCollection(this);
    }
}
