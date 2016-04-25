using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

    public Player player;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public GameObject Camera { get { return cam; } set { cam = value; } }
    private PlayerAnimations anim;


    private GameObject cam;
    private float totalXRotation;
    private Rigidbody rb;
    private bool isAttacking;
    public float cooldown;

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

        // Stomp
        /* DOTO: Implement */
        if (Input.GetKeyDown(KeyCode.Q) && !player.Grounded)
        {
            anim.CmdSetBool("Stomp", true);
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

        else if (Input.GetMouseButtonUp(1))
        {
            anim.CmdSetBool("Blocking", false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            anim.CmdSetTrigger("SidePunch");
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (anim.IsAnimationPlaying("LeftPunch"))
            {
                anim.CmdSetBool("Combo", true);
            }
            //else if(anim.IsAnimationPlaying("Kick"))
            //{
            //    anim.CmdSetBool("Combo", true);
            //}
            else
            {
                anim.CmdSetTrigger("LeftPunch");
                anim.CmdSetBool("Combo", false);
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!isAttacking)
            {
                anim.CmdSetTrigger("Hadouken");
                startAttacking(2f, 0.2f);
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            anim.CmdSetTrigger("RightLongPunch");
        }

        if (isAttacking)
        {
            cooldown -= Time.deltaTime;
            if(cooldown <= 0)
            {
                isAttacking = false;
                cooldown = 0;
                player.ReturnToDefaultSpeed();
            }
        }
        if (anim.IsAnimationPlaying("Stomp") && player.Grounded)
        {
            anim.CmdSetBool("Stomp", false);
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
        if (isLocalPlayer)
        {
            player.CmdTakeDamage(attack.damage);
            rb.AddForce(attack.direction * attack.force * (player.KnockoutPercent / 100) * 1000, ForceMode.Impulse);
        }
        
       //RpcPushPlayer(attack.direction, attack.force, player.KnockoutPercent);        
    }
    /*
    [ClientRpc]
    public void RpcPushPlayer(Vector3 direction, float force, float percent)
    {
        rb.AddForce(direction * force * (percent / 100) * 10000, ForceMode.Acceleration);
    }
    */
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

    void startAttacking(float cooldown, float playerSpeed)
    {
        this.cooldown = cooldown;
        isAttacking = true;
        player.MultiplySpeed(playerSpeed);
    }

    public void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation) as GameObject;
    }


}
