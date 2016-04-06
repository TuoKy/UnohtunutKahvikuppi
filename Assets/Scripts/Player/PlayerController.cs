using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

    public Player player;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public GameObject Camera { get { return cam; } set { cam = value; } }
    private PlayerAnimations anim;


    private GameObject cam;
    private float totalXRotation;
    private Rigidbody rb;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        player = GetComponent<Player>();
        GameManager.instance.SetPlayerLives(player.Lives);
        anim = GetComponent<PlayerAnimations>();
    }
	
	// Update is called once per frame
	void Update () {
        CalculateActualDirection();
        Rotation();

        // Jump
        if (Input.GetButtonDown("Jump") && player.Grounded)
        {
            rb.velocity += new Vector3(0, player.JumpStrength, 0);
            GetComponent<PlayerAnimations>().CmdSetBool("Jumping", true);
        }

        // Double Jump
        if (Input.GetButtonDown("Jump") && !player.Grounded && !player.DoubleJumped)
        {
            player.DoubleJumped = true;
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.velocity += new Vector3(0, player.JumpStrength, 0);
            anim.CmdSetTrigger("DoubleJumping");
        }

        // Animation
        if (player.Movement.x != 0 || player.Movement.z != 0)
        {
            anim.CmdSetBool("Moving", true);
        }
        else
        {
            anim.CmdSetBool("Moving", false);
        }

        if (Input.GetMouseButtonDown(1))
        {
            anim.CmdSetBool("Blocking", true);
        }

        if (Input.GetMouseButtonUp(1))
        {
            anim.CmdSetBool("Blocking", false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            anim.CmdSetTrigger("LeftPunch");
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            anim.CmdSetTrigger("Hadouken");
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            anim.CmdSetTrigger("RightLongPunch");
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.instance.ActivatePauseMenu();
        }
    }

    //TODO: Can player move while attacking?
    void FixedUpdate()
    {
        player.Movement = player.ActualDirection.normalized * Time.deltaTime * player.Speed;
        rb.velocity = new Vector3(player.Movement.x, rb.velocity.y, player.Movement.z);
    }

    public void GetHitByAttack(Attack attack)
    {
        player.TakeDamage(attack.damage);
        rb.AddForce(attack.direction * attack.force * (player.KnockoutPercent / 100) * 10000);
    }

    private void CalculateActualDirection()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);

        player.ActualDirection = transform.TransformDirection(direction);

        player.ActualDirection.Set(player.ActualDirection.x, 0, player.ActualDirection.z);
    }

    void Rotation()
    {
        totalXRotation = (Input.GetAxis("Mouse X"));

        if (Cursor.lockState == CursorLockMode.Locked)
        {
            transform.Rotate(0, totalXRotation * player.TurnSpeed, 0);
        }
    }

    public void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation) as GameObject;
    }
}
