using UnityEngine;
using System.Collections;

public class Player {

    private string characterName;
    private float knockoutPercent = 0f;
    private int score = 0;
    private float defaultSpeed = 400f;
    private float speed = 400f;
    private float jumpStrength = 15f;
    private bool grounded;

    public string CharacterName { get { return characterName; } set { characterName = value; } }
    public float KnockoutPercent { get { return knockoutPercent; } }
    public float Score { get { return score; } }
    public float Speed { get { return speed; } }
    public float JumpStrength { get { return jumpStrength; } }
    public bool Grounded { get { return grounded; } set { grounded = value; } }

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
}
