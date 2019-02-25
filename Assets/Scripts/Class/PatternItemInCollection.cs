using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PatternItemInCollection : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler {
    public bool IsAchetype = true;
    public bool draggable;
    public GameObject Archetype;
    public Vector2 originSize;
    public Vector3 localPos;
    public int PatternId;
    public void OnDrag(PointerEventData eventData)
    {
        if (draggable)
        {
            transform.position = Input.mousePosition;
        }
    }

    public void Set(PatternItemInCollection p) {
        PatternId = p.PatternId;
        localPos = new Vector3(p.localPos.x, p.localPos.y, p.localPos.z);
        originSize = new Vector2(p.originSize.x, p.originSize.y);
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
                //PatternCollectionManager.UpdatePo
                PatternManager.Instance.CheckDeletion(gameObject);
                transform.parent = PatternManager.Instance.PatternPaintingPanel;
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
