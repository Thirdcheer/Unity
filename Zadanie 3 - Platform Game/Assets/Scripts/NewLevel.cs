using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewLevel : MonoBehaviour {


    public int sceneIndex;
    public GameObject tryAgain;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other) {
        var objects = GameObject.FindGameObjectWithTag("Finish");

        if (other.gameObject.CompareTag("Player"))
        {
            if(sceneIndex == 99) {
                if (tryAgain != null && objects == null) {
                    tryAgain.SetActive(true);

                    var panel = GameObject.Instantiate(tryAgain);
                    Canvas cv = GameObject.FindObjectOfType<Canvas>();
                    panel.transform.parent = cv.transform;
                    panel.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
                    Debug.Log("Game over");
                }
            }
            else
                Application.LoadLevel(sceneIndex);
        }
    }

}
