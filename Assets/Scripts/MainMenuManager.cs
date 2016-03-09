using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

	public static MainMenuManager Instance { get; private set; }
    public Transform MainMenu, OptionsMenu;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitGame();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("ArenaScene");
    }

    public void OptionsScreen(bool isOptionsMenu)
    {
        OptionsMenu.gameObject.SetActive(isOptionsMenu);
        MainMenu.gameObject.SetActive(!isOptionsMenu);
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
