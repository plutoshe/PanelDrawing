using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PatternItemInCollection : MonoBehaviour, IDragHandler, IEndDragHandler {
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

    public void OnEndDrag(PointerEventData eventData)
    {
        if (draggable && IsAchetype)
        {
            PatternManager.Instance.DrawPaintingPanel(gameObject);    
            transform.position = Archetype.transform.position;
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
