using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerScore : NetworkBehaviour
{

    private Text countDownText;
    private int elapsedTime;
    private bool startFabulousText;

    private Transform spawnPoints;
    private List<Transform> spawnPointsList;
    private Rigidbody rig;
    private PlayerController controls;

    // Use this for initialization
    void Start () {
        InitSpawnPoints();
        countDownText = GameObject.Find("CountDownText").GetComponent<Text>();
        elapsedTime = 0;
        startFabulousText = false;

        rig = gameObject.GetComponent<Rigidbody>();
        controls = GetComponent<PlayerController>();
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
        rig.velocity = Vector3.zero;
        controls.Camera.GetComponent<CamController>().Falling = false;
        Vector3 temp = spawnPointsList[Random.Range(0, spawnPointsList.Capacity - 1)].position;
        GetComponent<PlayerSynchPos>().CmdTeleportOnServer(temp);
        gameObject.transform.position = temp;
        GetComponent<PlayerSynchPos>().CmdTeleportOnServer(temp);

    }

    public IEnumerator StartReSpawn()
    {
        if (controls.player.Lives > 0)
        {
            while (elapsedTime < 4)
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
            DeclareLoss();
        }

        countDownText.transform.localScale = new Vector3(1, 1, 1);
        elapsedTime = 0;
    }

    public void setCountDownText(string message)
    {
        countDownText.text = message;
    }

    public void DeclareLoss()
    {
        countDownText.text = "You lost";
        rig.useGravity = false;
        rig.velocity = Vector3.zero;
        controls.enabled = false;
    }
}
