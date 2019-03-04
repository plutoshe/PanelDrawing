using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

[Serializable]
public class PatternItemInCollection : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler {

    public bool IsEditing = true;
    public bool IsAchetype = true;
    public bool draggable;
    public GameObject Archetype;
    public Vector2 originSize;
    public Vector3 localPos;
    public int PatternId;
    public Material outlineMaterial;

    bool CouldDrag()
    {
        return IsEditing && IsAchetype && !PatternManager.Instance.OnEditingPattern() ||
            PatternManager.Instance.selectedPatternItem == transform;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (CouldDrag())
        {

            if (draggable)
            {
                transform.position = Input.mousePosition;

            }
        }
    }

    public void Set(PatternItemInCollection p) {
         
        IsAchetype = p.IsAchetype;
        PatternId = p.PatternId;
        localPos = new Vector3(p.localPos.x, p.localPos.y, p.localPos.z);
        originSize = new Vector2(p.originSize.x, p.originSize.y);
        draggable = p.draggable;
        Archetype = p.Archetype;
    
    
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (CouldDrag())
        {
            if (!IsAchetype)
            {
                transform.parent = PatternManager.Instance.PatternSamplePanel;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (CouldDrag())
        {
            if (draggable)
            {
                if (IsAchetype)
                {
                    PatternManager.Instance.DrawPaintingPanel(gameObject);
                    transform.position = Archetype.transform.position;
                }
                else
                {
                    //PatternCollectionManager.UpdatePo
                    PatternManager.Instance.CheckDeletion(gameObject);
                    transform.parent = PatternManager.Instance.PatternPaintingPanel;
                }
            }
        }
    }
    void UpdateSelectItemUI(bool isSelected)
    {
        if (isSelected)
        {
            print(outlineMaterial.name);
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


    bool isDrawingItem()
    {
        return !IsAchetype;
    }

    public void ToggleSelectItem()
    {
        if (isDrawingItem())
        {
            PatternManager.Instance.selectedPatternItem = transform;
            UpdateSelectItemUI(true);
        }
    }

    void OnSelection()
    {
        //print("!!!" + PatternManager.Instance.OnEditingPattern());
        if (IsEditing && !PatternManager.Instance.OnEditingPattern())
            ToggleSelectItem();
    }

    public void OnUnSelection()
    {
        UpdateSelectItemUI(false);
    }
	// Use this for initialization
	void Start () {
        GetComponent<Button>().onClick.AddListener(OnSelection);
	}
    public void UpdateUIForChange()
    {
        if (GetComponent<Image>().material)
        {
            GetComponent<Image>().material.SetVector("_ObjectScale", transform.localScale);
            GetComponent<Image>().material.SetVector("_ObjectRect", ((RectTransform)transform).rect.size);
        }
    }

    // Update is called once per frame
    void Update () {
        
    }
}
