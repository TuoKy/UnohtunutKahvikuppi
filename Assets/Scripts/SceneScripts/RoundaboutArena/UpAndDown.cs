using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class UpAndDown : NetworkBehaviour
{
    private float verticalSpeed = 0.2f;

    private float maxY = 6;
    public bool movingUp;

    [ClientCallback]
	void FixedUpdate () {
        MoveUpAndDown();
	}

    private void MoveUpAndDown()
    {
        if (movingUp)
        {
            if (gameObject.transform.position.y < maxY)   // Up
            {
                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, new Vector3(gameObject.transform.position.x, (gameObject.transform.position.y + verticalSpeed), gameObject.transform.position.z), verticalSpeed);
            }
            else
            {
                movingUp = false;
            }
        }
        else
        {
            if (gameObject.transform.position.y > -maxY)   // Down
            {
                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, new Vector3(gameObject.transform.position.x, (gameObject.transform.position.y - verticalSpeed), gameObject.transform.position.z), verticalSpeed);
            }
            else
            {
                movingUp = true;
            }
        }
    }
}
