using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatternManager : MonoBehaviour {

    static PatternManager _instance;
    public List<PatternAttr> Patterns;
    public Transform PatternDisplayPanel;
    public GameObject PatternItemPrefab;
    public static PatternManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new PatternManager();
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        if (_instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {
        for (int i = 0; i < Patterns.Count; i++)
        {
            var newPattern = Instantiate(PatternItemPrefab);
            newPattern.GetComponent<PatternItem>().attr = Patterns[i];
            newPattern.transform.parent = PatternDisplayPanel;
            newPattern.SetActive(true);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
