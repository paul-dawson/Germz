using UnityEngine;

public class PlayerSwitchingBasic : PlayerSwitching
{    
    new public void Awake()
    {
        base.Awake();

        //walkSpeed = 2;
        //walkBackwardSpeed = walkSpeed;
        //sidestepSpeed = walkSpeed;
 
        //jumpHeight = 2;

        
		
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerDeathTrigger die = other.gameObject.GetComponent<PlayerDeathTrigger>() as PlayerDeathTrigger;
        if (die != null)
        {
            Player.KillPlayer();
        }
    }
}