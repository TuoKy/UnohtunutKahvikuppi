using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public GameObject pausePanel;
    private static GameManager _instance;

    public static GameManager instance
    {
        get
        {
            //If _instance hasn't been set yet, we grab it from the scene!
            //This will only happen the first time this reference is used.
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<GameManager>();
            return _instance;
        }
    }

    // Use this for initialization
    void Start () {
        backToGame();
        /*Cursor.lockState = CursorLockMode.Locked;
        pausePanel.SetActive(false);*/
    }
	
    IEnumerable DelayPauseMenu()
    {
        yield return new WaitForSeconds(0.5f);
    }

	// Update is called once per frame
	void Update () {
	
	}

    public void ToggleCursorLock()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void ActivatePauseMenu()
    {
        pausePanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void backToGame()
    {
        pausePanel.SetActive(false);
        ToggleCursorLock();
    }
}
