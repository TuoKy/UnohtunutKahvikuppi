using UnityEngine;
using System.Collections;

public class HeadTransform : MonoBehaviour {

    public Transform playerTransform;
    public GameObject cam;
    public UnityEngine.UI.Text percentText;
    public Player playerInfo;


    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = playerTransform.position + new Vector3(0,3,0);
        transform.LookAt(cam.transform);
        percentText.text = playerInfo.KnockoutPercent.ToString() + " %";
    }
}
