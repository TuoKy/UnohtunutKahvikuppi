using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public GameObject LifeTokenPrefab;
    public Transform playerPercentPanel;
    public Text knockoutText;
    public Vector3 startPosition = new Vector3(90f, 50f, 0f);
    private static GameManager _instance;
    private List<GameObject> lifeTokens;

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
    void Start()
    {
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

    public void UpdateKnockoutPercent(float value)
    {
        knockoutText.text = value.ToString() + "%";
    }

    public void SetPlayerLives(int lives)
    {

        lifeTokens = new List<GameObject>();
        float posX = startPosition.x;
        Quaternion orientation = Quaternion.identity;

        for (int i = 0; i < lives; i++)
        {
            // Instatiate new token from prefab and put it in the ui-panel
            GameObject newLifeToken = Instantiate(LifeTokenPrefab, new Vector3(posX, startPosition.y, startPosition.z), orientation) as GameObject;
            newLifeToken.transform.SetParent(playerPercentPanel, false);
            lifeTokens.Add(newLifeToken);

            // Change x position for the next token
            posX += ((RectTransform)LifeTokenPrefab.transform).rect.width + 10f;
        }
    }

    public void TakePlayerLifeToken()
    {
        int index = lifeTokens.Count - 1;
        GameObject lastLife = lifeTokens[index];
        Destroy(lastLife);
        lifeTokens.RemoveAt(index);
    }
}
