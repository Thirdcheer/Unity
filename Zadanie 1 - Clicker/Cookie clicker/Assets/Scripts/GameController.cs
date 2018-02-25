using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    int numberOfCookies = 0;
    public GameObject TimeValue;
    public List<GameObject> cookies;
    public Canvas canvas;
    public GameObject panel;
    public GameObject resultLabel;

    // Use this for initialization
    void Start () {

        if (PlayerPrefs.HasKey("Number of cookies"))
        {
            numberOfCookies = PlayerPrefs.GetInt("Number of cookies");
        }
        else
        {
            Debug.Log("Non Existing Key");
            numberOfCookies = 0;
        }

        spawnCookies(numberOfCookies);
        Time.timeScale = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Time.timeScale = 1;
        }
        float time = Time.timeSinceLevelLoad;
        TimeValue.GetComponent<Text>().text = time.ToString("0.00");


        if(GameObject.FindGameObjectsWithTag("cookie").Length == 0)
        {

            Time.timeScale = 0;
            panel.SetActive(true);
            resultLabel.GetComponent<Text>().text = "You ate " + numberOfCookies + " cookies in " + TimeValue.GetComponent<Text>().text + " seconds.";
        }
    }

    void spawnCookies(int number)
    {
        float width = canvas.GetComponent<RectTransform>().rect.width/2;
        float height = canvas.GetComponent<RectTransform>().rect.height / 2;
        for (int x=1; x<number+1; x++)
        {
           
            Vector3 worldPos = new Vector3(Random.Range(-width, width), Random.Range(-height,height ),-1);
            Quaternion rotation = Quaternion.Euler(0, 0, Random.Range(0.0f, 360.0f));

            GameObject cookieObject = Instantiate(cookies[Random.Range(0,3)], worldPos, rotation) as GameObject;
            cookieObject.transform.SetParent(canvas.transform, false);
        }

        PlayerPrefs.SetInt("Current", number);
    }

     
}
