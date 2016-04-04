using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {

    public float force;
    public float damage;
    public float launch;
    public Vector3 direction;

    void Start()
    {
        direction = new Vector3();
    }

    public void UpdateDirection(Vector3 enemyRot)
    {
        direction = new Vector3(Mathf.Sin(Mathf.Deg2Rad * enemyRot.y), launch, Mathf.Cos(Mathf.Deg2Rad * enemyRot.y));
        //Debug.Log(enemyRot + ", " + direction);
    }
}
