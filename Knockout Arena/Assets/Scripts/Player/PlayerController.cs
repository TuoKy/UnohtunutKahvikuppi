using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed = 10f;
    public Vector3 actualDirection, movement;
    public Transform parent;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        CalculateActualDirection();
        movement = actualDirection * Time.deltaTime * speed;
    }

    void FixedUpdate()
    {
        // transform.parent.GetComponent<Rigidbody>().AddForce(movement, ForceMode.Force);
        transform.parent.GetComponent<Rigidbody>().velocity = movement;
    }

    private void CalculateActualDirection()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 direction = new Vector3(horizontalInput, 0.0f, verticalInput);
       
        actualDirection = transform.TransformDirection(direction);
        // TODO: Find neater solution
        actualDirection.Set(actualDirection.x, 0, actualDirection.z);
    }
}
