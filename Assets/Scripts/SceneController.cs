using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {
    static SceneController _instance;
    public static SceneController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SceneController();
            }
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
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadScene(string sceneName)
    {
        Debug.Log("Go to Scene:" + sceneName + "\n");
        SceneManager.LoadScene(sceneName);

    }

}
