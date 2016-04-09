using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SceneChange()
    {
        SceneManager.LoadScene("ArenaScene");
    }
}
