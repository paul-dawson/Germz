using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

    // GUI Elements
    private GameObject m_healthBar = null; 

    // Determines whether the game is in debug mode
    public bool DebugMode = false;

    public GUIStyle DebugTextStyle;

    // Player Specific Variables //
    private int m_currentHealth = Player.MaximumHealth;

    private int m_collectibleCount = 0;

    private string m_currentGerm = "Basic";

    // Use this for initialization
    void Start()
    {
        // Ensure any Engine Related Components are hidden when game is in play
        if (!DebugMode)
        {
            HideTriggerObjects();
        }

        m_collectibleCount = GameObject.FindGameObjectsWithTag("Collectible").Length;

        // Load GUI Elements
        m_healthBar = Instantiate(Resources.Load("gui_healthBar")) as GameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void HideTriggerObjects()
    {
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Trigger"))
        {
            gameObject.renderer.enabled = false;
        }
    }

    void OnGUI()
    {
        PlayerHealthBar();

        GUI.TextField(new Rect(28, 75, 150, 25), "Germ: " + m_currentGerm, DebugTextStyle);
        GUI.TextField(new Rect(28, 100, 150, 25), "Collectables: " + Player.GetScore().ToString() + " / " + m_collectibleCount, DebugTextStyle);
    }

    void SetGerm(string name)
    {
        m_currentGerm = name;
    }

    public static void ChangeGerm(string name)
    {
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>() as GameManager;
        gm.SetGerm(name);
    }

    #region PlayerGUI

    void PlayerHealthBar()
    {
        Rect textPosition = new Rect(28, 34, 100, 25);
        GUI.TextField(textPosition, "Health: " + m_currentHealth, GUIStyle.none);
    }

    #endregion
}
