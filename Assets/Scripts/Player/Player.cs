using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player : NetworkBehaviour
{

    private string characterName;
    //private float knockoutPercent = 0f;
    private int score = 0;
    private float defaultSpeed = 400f;
    private float speed = 400f;
    private float jumpStrength = 15f;
    private float turnSpeed = 10f;
    private bool grounded;
    private bool doubleJumped;
    private Vector3 actualDirection, movement;

    //public float KnockoutPercent { get { return knockoutPercent; } }
    public float Score { get { return score; } }
    public float Speed { get { return speed; } }
    public float JumpStrength { get { return jumpStrength; } }
    public float TurnSpeed { get { return turnSpeed; } }
    public bool Grounded { get { return grounded; } set { grounded = value; } }
    public bool DoubleJumped { get { return doubleJumped; } set { doubleJumped = value; } }
    public Vector3 ActualDirection { get { return actualDirection; } set { actualDirection = value; } }
    public Vector3 Movement { get { return movement; } set { movement = value; } }

    [SyncVar]
    public Color color;
    [SyncVar]
    public string playerName;
    [SyncVar]
    public int Lives;
    [SyncVar]
    public float KnockoutPercent = 0f;

    void Start()
    {

    }

    public Player()
    {

    }

    public Player(string name, float speed, float jumpStrength)
    {
        this.characterName = name;
        this.speed = speed;
        this.jumpStrength = jumpStrength;
    }

    public void ResetKnockoutPercent()
    {
        KnockoutPercent = 0f;
    }

    [Command]
    public void CmdTakeDamage(float damage)
    {
        KnockoutPercent += damage;
    }

    public void ReturnToDefaultSpeed()
    {
        speed = defaultSpeed;
    }

    public void MultiplySpeed(float multiplier)
    {
        speed *= multiplier;
    }

    public void ScorePoint()
    {
        score++;
    }

    public void LosePoint()
    {
        score--;
    }

    [Server]
    public void LoseLive()
    {
        Lives--;
    }

}
