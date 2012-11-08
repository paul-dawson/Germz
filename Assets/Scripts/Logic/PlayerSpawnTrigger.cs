using UnityEngine;

public class PlayerSpawnTrigger : Trigger
{
    public GameObject PlayerObject;
    public float HeightDifferential = 0.5f;

    new void Awake() 
    {
        base.Awake();

        GameObject Player = Instantiate(PlayerObject, m_transform.position + (Vector3.up * HeightDifferential), m_transform.rotation) as GameObject;

        // Retrieve the camera object and set its target to the newly instantiated game object
        TPCamera Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent("TPCamera") as TPCamera;
        Camera.Target = Player;
    }
}
