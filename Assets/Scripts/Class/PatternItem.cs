
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class PatternAttr
{
    public Sprite DisplayImage;
}
[RequireComponent(typeof(Image))]
public class PatternItem : MonoBehaviour
{
    public PatternAttr attr;
    // Use this for initialization
    void Start()
    {
        GetComponent<Image>().sprite = attr.DisplayImage;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
