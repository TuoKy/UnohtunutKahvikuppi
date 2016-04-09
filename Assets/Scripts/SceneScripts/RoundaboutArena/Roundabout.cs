using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Roundabout : MonoBehaviour {

    public List<GameObject> planes;
    private List<Vector3> targets;
    private int round; // there are 4 disk and therefore 4 rounds

	void Start () {
        targets = new List<Vector3>();
        round = 0;
        for (int i = 0; i < planes.Count; i++)
        {
            targets.Add(planes[i].transform.position);
        }      
    }
	
	void Update () {
	
	}

    void FixedUpdate()
    {

            
        /*TODO: create real solution dammit
            for(int i = 0; i < planes.Count; i++)
            {
                if(i+1+round < 4)
                    planes[i].transform.position = Vector3.Lerp(planes[i].transform.position, targets[round+i+1].position, Time.deltaTime * 0.6f);
                else
                    planes[i].transform.position = Vector3.Lerp(planes[i].transform.position, targets[0].position, Time.deltaTime * 0.6f);
            }
         */
        if(round == 0) { 
            planes[0].transform.position = Vector3.Lerp(planes[0].transform.position, targets[1], Time.deltaTime * 0.6f);
            planes[1].transform.position = Vector3.Lerp(planes[1].transform.position, targets[2], Time.deltaTime * 0.6f);
            planes[2].transform.position = Vector3.Lerp(planes[2].transform.position, targets[3], Time.deltaTime * 0.6f);
            planes[3].transform.position = Vector3.Lerp(planes[3].transform.position, targets[0], Time.deltaTime * 0.6f);
            if (Vector3.Distance(planes[0].transform.position, targets[1]) < 1)
                round = 1;
        }
        
        if (round == 1)
        {
            planes[0].transform.position = Vector3.Lerp(planes[0].transform.position, targets[2], Time.deltaTime * 0.6f);
            planes[1].transform.position = Vector3.Lerp(planes[1].transform.position, targets[3], Time.deltaTime * 0.6f);
            planes[2].transform.position = Vector3.Lerp(planes[2].transform.position, targets[0], Time.deltaTime * 0.6f);
            planes[3].transform.position = Vector3.Lerp(planes[3].transform.position, targets[1], Time.deltaTime * 0.6f);
            if (Vector3.Distance(planes[0].transform.position, targets[2]) < 1)
                round = 2;
        }
        if (round == 2)
        {
            planes[0].transform.position = Vector3.Lerp(planes[0].transform.position, targets[3], Time.deltaTime * 0.6f);
            planes[1].transform.position = Vector3.Lerp(planes[1].transform.position, targets[0], Time.deltaTime * 0.6f);
            planes[2].transform.position = Vector3.Lerp(planes[2].transform.position, targets[1], Time.deltaTime * 0.6f);
            planes[3].transform.position = Vector3.Lerp(planes[3].transform.position, targets[2], Time.deltaTime * 0.6f);
            if (Vector3.Distance(planes[0].transform.position, targets[3]) < 1)
                round = 3;
        }
        if (round == 3)
        {
            planes[0].transform.position = Vector3.Lerp(planes[0].transform.position, targets[0], Time.deltaTime * 0.6f);
            planes[1].transform.position = Vector3.Lerp(planes[1].transform.position, targets[1], Time.deltaTime * 0.6f);
            planes[2].transform.position = Vector3.Lerp(planes[2].transform.position, targets[2], Time.deltaTime * 0.6f);
            planes[3].transform.position = Vector3.Lerp(planes[3].transform.position, targets[3], Time.deltaTime * 0.6f);
            if (Vector3.Distance(planes[0].transform.position, targets[0]) < 1)
                round = 0;
        } 
           
    }
}
