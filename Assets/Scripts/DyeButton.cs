﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DyeButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateColor()
    {
        print(GetComponent<Image>().color);
        transform.parent.Find("DyingPanel").GetComponent<DrawInBoard>().ChangeColor(
            GetComponent<Image>().color
            );
    }
}