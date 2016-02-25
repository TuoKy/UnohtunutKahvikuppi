using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerScore : MonoBehaviour {

    private Transform spawnPoints;
    private List<Transform> spawnPointsList;
    
    // Use this for initialization
	void Start () {
        InitSpawnPoints();
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
}
