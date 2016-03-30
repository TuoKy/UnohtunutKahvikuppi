using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour {

    private Text countDownText;
    private int elapsedTime;
    private bool startFabulousText; 

    private Transform spawnPoints;
    private List<Transform> spawnPointsList;
    
    // Use this for initialization
	void Start () {
        InitSpawnPoints();
        countDownText = GameObject.Find("CountDownText").GetComponent<Text>();
        elapsedTime = 0;
        startFabulousText = false;
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

    void FixedUpdate()
    {
        if(startFabulousText)
            countDownText.transform.localScale += Vector3.Lerp(countDownText.transform.localScale, new Vector3(1, 1, 1), 1);
    }

    public void ReSpawn()
    {
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<PlayerController>().Camera.GetComponent<NewCamController>().Falling = false;
        Vector3 temp = spawnPointsList[Random.Range(0, spawnPointsList.Capacity - 1)].position;
        GetComponent<PlayerSynchPos>().CmdTeleportOnServer(temp);
        gameObject.transform.position = temp;
        GetComponent<PlayerSynchPos>().CmdTeleportOnServer(temp);

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
                
                startFabulousText = true;
                yield return new WaitForSeconds(1.0f);
                startFabulousText = false;
                countDownText.transform.localScale = new Vector3(1, 1, 1);
                elapsedTime++;
            }
           
            countDownText.text = "";
            ReSpawn();
        }
        else
        {
            countDownText.text = "You lost";
            GetComponent<Rigidbody>().useGravity = false;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero; 
            GetComponent<PlayerController>().enabled = false;
            NetGameManager.instance.CmdRemovePlayerFromList(gameObject);
        }

        countDownText.transform.localScale = new Vector3(1, 1, 1);
        elapsedTime = 0;
    }

    public void setCountDownText(string message)
    {
        countDownText.text = message;
    }

    public void KillMe()
    {
        GetComponent<PlayerController>().player.Lives -= 1;
    }
}
