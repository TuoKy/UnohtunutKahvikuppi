using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed = 10f;
    public Vector3 actualDirection, movement;
    public Transform parent;

    public float acceleration = 150f;
    public float maxSpeed = 5f;

    private float distToGround;

    private Rigidbody rb;

    // for jumping
    public float jumpStrength = 500f;
    private bool grounded;
    private Collider[] groundCollisions;
    private float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    private Transform groundCheck;

    // Use this for initialization
    void Start () {
        rb = transform.parent.GetComponent<Rigidbody>();
        distToGround = transform.parent.GetComponent<Collider>().bounds.extents.y;
        groundCheck = gameObject.transform;

    }
	
	// Update is called once per frame
	void Update () {
        CalculateActualDirection();

        //movement = actualDirection * Time.deltaTime * speed;
        movement = actualDirection.normalized * acceleration;
        //movement = actualDirection * acceleration;

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

        groundCollisions = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, groundLayer);
        if (groundCollisions.Length > 0) grounded = true;
        else grounded = false;

        //grounded = IsGrounded();
        
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

    /*void OnCollisionStay(Collision info)
    {
        grounded = true;
    }

    void OnCollisionExit(Collision info)
    {
        grounded = true;

    }*/

    // check if player touches ground
    bool IsGrounded()
    {
        //int layerMask = 1 << LayerMask.NameToLayer("Ground");

        //Bounds meshBounds = transform.parent.GetComponent<MeshFilter>().mesh.bounds;

        /*if (Physics.Raycast(transform.parent.transform.position+ meshBounds.center, Vector3.down, meshBounds.extents.y, layerMask))
        {
            return true;
        }*/
        //return false;

        return Physics.Raycast(transform.parent.transform.position, Vector3.down, distToGround + 0.1f);
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
