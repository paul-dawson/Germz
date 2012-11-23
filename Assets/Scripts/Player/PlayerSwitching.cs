using UnityEngine;
using System;

//[RequireComponent(typeof(Rigidbody))]
//[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(PlatformInputController))]
public class PlayerSwitching : MonoBehaviour
{

    public static T Clamp<T>(T value, T max, T min)
         where T : System.IComparable<T>
    {
        T result = value;
        if (value.CompareTo(max) < 0)
            result = max;
        if (value.CompareTo(min) > 0)
            result = min;
        return result;
    }



   // protected double walkSpeed = 2.0;
  //  protected double walkBackwardSpeed = 4.0;
  //  protected double sidestepSpeed = 8.0;

   // protected double maxVelocityChange = 10.0;
    //protected double inAirControl = 0.1;
    //protected bool canJump = true;
    //protected double jumpHeight = 2.0;

    //bool canRun = true;
    //double runSpeed = 14.0; // negative values here makes game unhappy
    //double runBackwardSpeed = 6.0; // negative values here makes game unhappy
    //bool canRunSidestep = true;
    //double runSidestepSpeed = 12.0;

    //Vector3 groundVelocity;
    //CapsuleCollider[] capsule = new CapsuleCollider[1];


    public bool firstCharacter = false;
    public AudioClip soundLanding,soundWalking;
    //public bool rollingMovement = false;
	
	

    //GameObject spriteManager;


    CharacterMotor characMotor;
   
	private bool oldGrounded = false;

    
    protected static GameObject characBasic,characMelee,characDefense,characPropulsion,characRanged;

   // bool grounded = false;
    


    static bool characterLoaded = false;

    public void Awake()
    {
		
        if (!characterLoaded)
        {
          //  spriteManager = GameObject.Find("SpriteManager");
            characBasic = GameObject.Find("characBasic");
            characMelee = GameObject.Find("characMelee");
            characDefense = GameObject.Find("characDefense");
            characPropulsion = GameObject.Find("characPropulsion");
            characRanged = GameObject.Find("characRanged");

            characterLoaded = true;
			
        }

        if (!firstCharacter)
        {
            gameObject.SetActiveRecursively(false);
        }
		else
		{
			Player.setPlayerGameObject(gameObject);
		}



        characMotor = (CharacterMotor)GetComponent("CharacterMotor");
        

        //rigidbody.freezeRotation = true;
        //rigidbody.useGravity = true;
        //rigidbody.isKinematic = false;
        //capsule[0] = (CapsuleCollider)GetComponent("CapsuleCollider");
		
		
        //if (rollingMovement)
        //{
        //    rigidbody.freezeRotation = false;
        //}
		
		
    }

    public void FixedUpdate()
    {
        //Vector3 targetVelocity;
        //if (grounded)
        //{

        //    targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //    if (Input.GetAxis("Vertical") > 0)
        //    {
        //        targetVelocity.x *= (float)((canRun && Input.GetButton("Fire2")) ? runSpeed : walkSpeed);
        //    }
        //    else
        //    {
        //        targetVelocity.x *= (float)((canRun && Input.GetButton("Fire2")) ? runBackwardSpeed : walkBackwardSpeed);
        //    }

        //    targetVelocity.z *= (float)((canRunSidestep && Input.GetButton("Fire2")) ? runSidestepSpeed : sidestepSpeed);




        //    var velocity = rigidbody.velocity;
        //    var velocityChange = (targetVelocity - velocity) + groundVelocity;
        //    velocityChange.y = 0;

        //    //Change to addForce if you are not rotating
        //    rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);

        //    // Jump
        //    if (canJump && Input.GetButton("Jump"))
        //    {
        //        rigidbody.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
        //    }

        //    grounded = false;
        //}

        //else
        //{
         // Add in air

            // Calculate how fast we should rotate
            /*rotation = Input.GetAxis("Horizontal") * rotateSpeed;
        
            // Set 'Horizontal' input behavior based on whether we can rotate in air
            targetVelocity = (rotateInAir) ? new Vector3(0, rotation, Input.GetAxis("Vertical"))
            : new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            */
        //    targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //    targetVelocity = transform.TransformDirection(targetVelocity) * (float)inAirControl;

        //    // Rotate if rotate in air is enabled.
        //    /*if (rotateInAir)
        //    {
        //        transform.Rotate(0, rotation, 0);
        //    }
        //    */

        //    rigidbody.AddForce(targetVelocity, ForceMode.VelocityChange);
        //}



        /*
        * Change character
        */
        /*if (Input.GetButton("switchRanged"))
        {
            this.changeCharacterTo(characRanged);
            GameManager.ChangeGerm("Ranged");	
        }
        else*/
        if (Input.GetButton("switchBasic"))
        {
            this.changeCharacterTo(characBasic);
            GameManager.ChangeGerm("Basic");	
        }
        else if (Input.GetButton("switchPropulsion"))
        {
            this.changeCharacterTo(characPropulsion);
            GameManager.ChangeGerm("Propulsion");	
        }
            /*
        else if (Input.GetButton("switchDefense"))
        {
            this.changeCharacterTo(characDefense);
            GameManager.ChangeGerm("Defence");	
        }
        else if (Input.GetButton("switchMelee"))
        {
            this.changeCharacterTo(characMelee);
            GameManager.ChangeGerm("Melee");	
        }
        */


        // We apply gravity manually for more tuning control
        //rigidbody.AddForce(new Vector3(0, (float)-gravity * rigidbody.mass, 0));

        
		if (characMotor.IsGrounded() != oldGrounded)
        {
			if (characMotor.IsGrounded())
			{
				collisionEnter();
			}
			else
			{
				collisionExit();
			}
			oldGrounded = characMotor.IsGrounded();
        }
		else
		{
			//use OnControllerColliderHit
		}


    }








    private void changeCharacterTo(GameObject newOne)
    {
        if (newOne != null)
        {
            gameObject.SetActiveRecursively(false);

            Player.setPlayerGameObject(newOne);
            newOne.transform.position = gameObject.transform.position;
            newOne.transform.rotation = gameObject.transform.rotation;

            CharacterMotor setVelocity = (CharacterMotor)newOne.GetComponent("CharacterMotor");
            setVelocity.SetVelocity((characMotor).movement.velocity);


            newOne.SetActiveRecursively(true);
        }
    }


    public void OnControllerColliderHit (ControllerColliderHit hit) {
      audio.PlayOneShot(soundWalking);
		
	}

    







    public void TrackGrounded(Collision col)
    {
        //var minimumHeight = capsule[0].bounds.min.y + capsule[0].radius;
        //foreach (ContactPoint c in col.contacts)
        //{
        //    if (c.point.y < minimumHeight)
        //    {
        //        //we track velocity for rigidbodies we're standing on
        //        if (col.rigidbody) groundVelocity = col.rigidbody.velocity;
        //        //and become children of non-rigidbody colliders
        //        else transform.parent = col.transform;
        //        grounded = true;
        //    }
        //}
		
    }

    //unparent if we are no longer standing on our parent
    public void collisionExit()
    {
		//audio.PlayOneShot(soundLanding);
        //if (col.transform == transform.parent) transform.parent = null;
    }

    public void OnCollisionStay(Collision col)
    {
        TrackGrounded(col);
        //if (col.gameObject.CompareTag("Ground"))
        //{
        //    //Spread infection
        //}

        


    }

    public void collisionEnter()
    {
       
        //if (col.gameObject.CompareTag("Player"))
        //{
        //    //TODO should not add component, but attach to it
        //    // 		col.gameObject.AddComponent("RigidbodyFPSWalker");
        //    rigidbody.AddForce(new Vector3(0, 100, 0));
        //}


        audio.PlayOneShot(soundLanding);

    }
}