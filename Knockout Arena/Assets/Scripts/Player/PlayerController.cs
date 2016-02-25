using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

    public float speed = 400f;
    public Vector3 actualDirection, movement;

    private Rigidbody rb;

    // for jumping
    public float jumpStrength = 60f;
    private bool grounded;

    //For attacks
    public GameObject attackHitboxLeftHand;
    public GameObject attackHitboxRightHand;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        CalculateActualDirection();
        movement = actualDirection.normalized * Time.deltaTime * speed;

        // Jump
        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb.velocity += new Vector3(0, jumpStrength, 0);
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
    }

    private void CalculateActualDirection()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);

        actualDirection = transform.TransformDirection(direction);

        actualDirection.Set(actualDirection.x, 0, actualDirection.z);

    }

    void OnCollisionStay(Collision info)
    {
        // check if player touches ground
        if (info.gameObject.CompareTag("Ground"))
        grounded = true;
    }

    void OnCollisionExit(Collision info)
    {
        if (info.gameObject.CompareTag("Ground"))
            grounded = false;
    }

}
