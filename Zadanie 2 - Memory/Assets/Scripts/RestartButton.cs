using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour {

    public Color highlightColor = Color.yellow;

    

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void onMouseDown()
    {
        Debug.Log("Clicked");
        SceneManager.LoadScene("Main scene");
    }

}
