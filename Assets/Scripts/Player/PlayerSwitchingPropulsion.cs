using UnityEngine;
public class PlayerSwitchingPropulsion : PlayerSwitching
{
    public int propulsionMaxPower = 100;
	public int propulsionLossPower = 10;
    public float propulsionPower = 1.0f;
	public float maxSpeed = 100;
    public int propulsionGainPower = 1;
	
	
	private int propulsionCurrentPower;
    private float lastTimeUsed;
    private float delayBetweenAction = 0.1f;
    private float lastTimeLosePower;
    private float delayBetweenLostPower = 0.1f;
    private float lastTimeReload;
    private float delayBetweenReload = 0.1f;

    new public void Awake()
    {
        base.Awake();
		propulsionCurrentPower = propulsionMaxPower;
        //walkSpeed = 5.0;
       
        //jumpHeight = 10;
        GameManager.ChangeGerm("Propulsion");	
    }
	
	
	
	 public void FixedUpdate()
    {
        base.FixedUpdate();


        if ((Time.realtimeSinceStartup - lastTimeReload) > delayBetweenReload)
        {
            propulsionCurrentPower += propulsionGainPower;
            lastTimeReload = Time.realtimeSinceStartup;
        }


        if (Input.GetButton("usePropulsion"))
        {
            usePropulsion();
        }


        propulsionCurrentPower = Clamp(propulsionCurrentPower, 0, propulsionMaxPower);
    }
	
	
	private void usePropulsion()
	{
        if (propulsionCurrentPower > propulsionLossPower && (Time.realtimeSinceStartup - lastTimeUsed) > delayBetweenAction)
        {
            CharacterMotor characMotor = (CharacterMotor)GetComponent("CharacterMotor");
			if (characMotor.movement.velocity.y < maxSpeed)
			{
            	characMotor.SetVelocity(new Vector3(
					characMotor.movement.velocity.x,
					Clamp (characMotor.movement.velocity.y + propulsionPower,0,maxSpeed),
					characMotor.movement.velocity.z));
			}
            
            lastTimeUsed = Time.realtimeSinceStartup;

            if ((Time.realtimeSinceStartup - lastTimeLosePower) > delayBetweenLostPower)
            {
                propulsionCurrentPower -= propulsionLossPower;
                lastTimeLosePower = Time.realtimeSinceStartup;
            }
        }
	}
	
}