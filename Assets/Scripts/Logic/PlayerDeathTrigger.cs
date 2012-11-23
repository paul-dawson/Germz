using UnityEngine;
using System.Collections;

public class PlayerDeathTrigger : Trigger {

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger HIT!");

        if (other.gameObject.tag == "Player")
        {
            Player.KillPlayer();

            Debug.Log("Player collide!!");
        }
    }
}
