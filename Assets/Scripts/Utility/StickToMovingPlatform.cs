using UnityEngine;
using System.Collections;

public class StickToMovingPlatform : MonoBehaviour {



    void OnCollisionExit(Collision info)
    {
        info.gameObject.transform.parent = null;
    }

    void OnCollisionEnter(Collision info)
    {
        if (info.gameObject.CompareTag("Player"))
        {
            info.gameObject.transform.parent = gameObject.transform;
        }
    }
}
