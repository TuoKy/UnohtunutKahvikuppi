using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

    public Player player;

    private Rigidbody rb;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        player = new Player();
    }
	
	// Update is called once per frame
	void Update () {
        CalculateActualDirection();
        

        // Jump
        if (Input.GetButtonDown("Jump") && player.Grounded)
        {
            rb.velocity += new Vector3(0, player.JumpStrength, 0);
        }

        // Animation
        if (player.Movement.x != 0 || player.Movement.z != 0)
        {
            GetComponent<PlayerAnimations>().CmdSetBool("Moving", true);
        }
        else
        {
            GetComponent<PlayerAnimations>().CmdSetBool("Moving", false);
        }
    }

    //TODO: Can player move while attacking?
    void FixedUpdate()
    {
        player.Movement = player.ActualDirection.normalized * Time.deltaTime * player.Speed;
        rb.velocity = new Vector3(player.Movement.x, rb.velocity.y, player.Movement.z);
    }

    private void CalculateActualDirection()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);

        player.ActualDirection = transform.TransformDirection(direction);

        player.ActualDirection.Set(player.ActualDirection.x, 0, player.ActualDirection.z);

    }
}
