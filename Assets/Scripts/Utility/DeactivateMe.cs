using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class DeactivateMe : NetworkBehaviour{

    private float cooldown;	
	
	void Update () {

        
        if(cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        else
        {
            RpcDeactivate();
        }

	}

    void RpcDeactivate()
    { 
        this.gameObject.SetActive(false);
    }

    public void setCooldown(float seconds)
    {
        this.cooldown = seconds;
    }
}
