using UnityEngine;
using System.Collections;

public class Player {

    private string characterName;
    private float knockoutPercent = 0f;
    private int score = 0;
    private float defaultSpeed = 400f;
    private float speed = 400f;
    private float jumpStrength = 15f;
    private float turnSpeed = 10f;
    private bool grounded;
    private bool doubleJumped;
    private Vector3 actualDirection, movement;
    private int lives = 3; //Default value. Remember to add option somewhere for players to adjust

    public string CharacterName { get { return characterName; } set { characterName = value; } }
    public float KnockoutPercent { get { return knockoutPercent; } }
    public float Score { get { return score; } }
    public float Speed { get { return speed; } }
    public float JumpStrength { get { return jumpStrength; } }
    public float TurnSpeed { get { return turnSpeed; } }
    public bool Grounded { get { return grounded; } set { grounded = value; } }
    public bool DoubleJumped { get { return doubleJumped; } set { doubleJumped = value; } }
    public Vector3 ActualDirection { get { return actualDirection; } set { actualDirection = value; } }
    public Vector3 Movement { get { return movement; } set { movement = value; } }

    public int Lives { get { return lives; } set { lives = value; } }

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
        knockoutPercent = 0f;
    }

    public void TakeDamage(float damage)
    {
        knockoutPercent += damage;
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
    public void LoseLive()
    {
        lives--;
    }
}
