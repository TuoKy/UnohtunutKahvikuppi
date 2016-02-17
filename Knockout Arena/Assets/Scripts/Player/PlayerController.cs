using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed = 10f;
    public Vector3 actualDirection, movement;
    public Transform parent;

    public float acceleration = 20f;
    public float maxSpeed = 10f;

    public float jumpStrength = 150f;
    private bool grounded;

    private Rigidbody rb;

    // Use this for initialization
    void Start () {
        rb = transform.parent.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        CalculateActualDirection();

        //movement = actualDirection * Time.deltaTime * speed;
        //movement = actualDirection.normalized * acceleration;
        movement = actualDirection * acceleration;

        // transform.parent.GetComponent<Rigidbody>().velocity = movement;

        //rb.velocity = movement;

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
        // transform.parent.GetComponent<Rigidbody>().AddForce(movement, ForceMode.Force);
        //transform.parent.GetComponent<Rigidbody>().velocity = movement;

        grounded = IsGrounded();
    }

    private void CalculateActualDirection()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        //Vector3 direction = new Vector3(horizontalInput, 0.0f, verticalInput);
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);

        actualDirection = transform.TransformDirection(direction);
        // TODO: Find neater solution
        actualDirection.Set(actualDirection.x, 0, actualDirection.z);
        //actualDirection.Set(actualDirection.x, actualDirection.y, actualDirection.z);
    }

    // make player jump
    void Jump()
    {
        //rb.velocity = new Vector3(rb.velocity.x, jumpStrength, rb.velocity.z);
        rb.AddForce(new Vector3(0, jumpStrength, 0));
    }

    // check if player touches ground
    bool IsGrounded()
    {

        return false;
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
