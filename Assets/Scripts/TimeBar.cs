using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBar : MonoBehaviour
{


    public Slider TB;
    public Button again;


    float _t = 0;
    public float videoTime;

    void Start()
    {
        _t = 0;
        TB.value = 0;
        
       again.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        _t = 0;
        TB.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _t += Time.deltaTime;
        if (_t <= 32f)
        {
            TB.value = _t / videoTime;
        }
    }
}
