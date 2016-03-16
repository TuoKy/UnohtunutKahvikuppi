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
        gameObject.transform.position = spawnPointsList[Random.Range(0, spawnPointsList.Capacity - 1)].position; 
    }

    public void StartReSpawn()
    {
        StartCoroutine(PauseBetweenRespawn());
    }

    IEnumerator PauseBetweenRespawn()
    {
        while (elapsedTime < 6)
        {            
            countDownText.text = elapsedTime.ToString();
            yield return new WaitForSeconds(1f);
            elapsedTime++;
        }
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        countDownText.text = "";
        ReSpawn();
    }
}
