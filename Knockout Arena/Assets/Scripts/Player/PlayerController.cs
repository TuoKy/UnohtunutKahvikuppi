using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

    public float speed = 10f;
    public Vector3 actualDirection, movement;

    public float acceleration = 150f;
    public float maxSpeed = 5f;

    private float distToGround;

    private Rigidbody rb;

    // for jumping
    public float jumpStrength = 500f;
    private bool grounded;
    private float groundCheckRadius = 0.2f;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }
	
	// Update is called once per frame
	void Update () {
        CalculateActualDirection();

        movement = actualDirection.normalized * acceleration;

        rb.AddForce(movement);

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        rb.AddForce(Physics.gravity * rb.mass);

        LimitVelocity();
        
    }

    void FixedUpdate()
    {
        
    }

    private void CalculateActualDirection()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);

        actualDirection = transform.TransformDirection(direction);
        // TODO: Find neater solution
        actualDirection.Set(actualDirection.x, 0, actualDirection.z);

    }

    void Jump()
    {
        if(grounded)
        rb.AddForce(new Vector3(0, jumpStrength, 0));
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

    // limit player's velocity in xz-axis
    void LimitVelocity()
    {
        Vector2 xzVel = new Vector2(rb.velocity.x, rb.velocity.z);
        if(xzVel.magnitude > maxSpeed)
        {
            xzVel = xzVel.normalized * maxSpeed;
            rb.velocity = new Vector3(xzVel.x, rb.velocity.y, xzVel.y);
        }
    }
}
