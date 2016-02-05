using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

	public static MainMenuManager Instance { get; private set; }

    public void StartGame()
    {
        SceneManager.LoadScene("ArenaScene");
    }

    public void OptionsScreen()
    {
        //TODO: Switch to options scene? Or activate & deactivate different buttons.
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
