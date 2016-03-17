using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FastAndFuriousTesting : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (!GameObject.Find("NetworkManager"))
            SceneManager.LoadScene(0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
