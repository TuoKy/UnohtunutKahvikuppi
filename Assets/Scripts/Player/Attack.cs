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

    public void UpdateDirection(Vector3 heading)
    {
        float distance = heading.magnitude;
        direction = heading / distance;
    }
}
