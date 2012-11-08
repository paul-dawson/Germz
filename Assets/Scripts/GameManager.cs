using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

    // Determines whether the game is in debug mode
    public bool DebugMode = false;

    // Use this for initialization
    void Start()
    {
        // Ensure any Engine Related Components are hidden when game is in play
        if (!DebugMode)
        {
            HideTriggerObjects();
        }
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
}
