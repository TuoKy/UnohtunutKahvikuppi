using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {

    public float force;
    public float damage;
    public Vector3 direction;

    void Start()
    {
        direction = new Vector3();
    }

    // Update is called once per frame
    void Update () {
        UpdateDirection();
	}

    public void UpdateDirection()
    {
        direction = new Vector3();
    }
}
