using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PatternItemInCollection : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler {
    public bool IsAchetype = true;
    public bool draggable;
    public GameObject Archetype;
    public void OnDrag(PointerEventData eventData)
    {
        if (draggable)
        {
            transform.position = Input.mousePosition;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!IsAchetype)
        {
            transform.parent = PatternManager.Instance.PatternSamplePanel;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (draggable)
        {
            if (IsAchetype)
            {
                PatternManager.Instance.DrawPaintingPanel(gameObject);
                transform.position = Archetype.transform.position;
            } else {
                PatternManager.Instance.CheckDeletion(gameObject);
                transform.parent = PatternManager.Instance.PattrenPaintingPanel;
            }
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
