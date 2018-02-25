using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour {

    public GameObject input;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void LoadScene(int scene)
    {
        string t = input.GetComponent<InputField>().text;
        int numberOfCookies;
        Int32.TryParse(t, out numberOfCookies);
        PlayerPrefs.SetInt("Number of cookies", numberOfCookies);

        Application.LoadLevel(scene);
    }
}
