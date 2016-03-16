using UnityEngine;
using System.Collections;

public class HadoukenController : MonoBehaviour {

    public float speed = 10;
    private Rigidbody rb;
    private PlayerController player;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        player = FindObjectOfType<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
        //rb.velocity = new Vector3(0, 0, speed);
	}
}
