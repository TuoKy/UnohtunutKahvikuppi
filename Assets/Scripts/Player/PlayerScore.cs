using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour {

    private Text countDownText;
    private int elapsedTime;

    private Transform spawnPoints;
    private List<Transform> spawnPointsList;
    
    // Use this for initialization
	void Start () {
        InitSpawnPoints();
        countDownText = GameObject.Find("CountDownText").GetComponent<Text>();
        elapsedTime = 0;
    }
	
    private void InitSpawnPoints()
    {
        spawnPoints = GameObject.Find("SpawnPoints").transform;
        spawnPointsList = new List<Transform>();

        for (int i = 0; i < spawnPoints.childCount; i++)
        {
            spawnPointsList.Add(spawnPoints.GetChild(i));
        }
    }

	// Update is called once per frame
	void Update () {
	
	}

    public void ReSpawn()
    {
        GetComponent<PlayerController>().Camera.GetComponent<NewCamController>().Falling = false;     
        gameObject.transform.position = spawnPointsList[Random.Range(0, spawnPointsList.Capacity - 1)].position; 
    }

    public void StartReSpawn()
    {
        //Add various stuff regarding death and respawn here

        //Start countdown before respawning
        StartCoroutine(PauseBetweenRespawn());
    }

    IEnumerator PauseBetweenRespawn()
    {
        if (GetComponent<PlayerController>().player.Lives > 0)
        {
            while (elapsedTime < 6)
            {            
            countDownText.text = elapsedTime.ToString();
            yield return new WaitForSeconds(0.5f);
            elapsedTime++;
            }

            GetComponent<PlayerController>().player.Lives -= 1;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            countDownText.text = "";
            ReSpawn();
        }
        else
        {
           countDownText.text = "You lost";
           GetComponent<Rigidbody>().useGravity = false;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero; 
           GetComponent<PlayerController>().enabled = false;
        }        
        elapsedTime = 0;
    }
}
