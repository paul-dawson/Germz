/*
 * ===== GERMZ ===========================================
 * 
 * Player
 * -------------------------------------------------------
 * The base Player class
 * -------------------------------------------------------
 * @author Paul Dawson <k0066812>
 * @created 08-Nov-2012 21:14
 *
 */

using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
    // PUBLIC PROPERTIES //

 //   public float MovementSpeed = 6.0f;
 //   public float JumpSpeed = 8.0f;
 //   public float Gravity = -9.81f;

    // STATIC PROPERTIES //

    public static int MaximumHealth = 100;
	public static int currentHealth;
	public static GameObject playerGameObject;
    public static int Score = 0;

    public static Vector3 playerSpawnPosition;

    // PRIVATE VARIABLES //

   // private Transform m_transform;
   // private Vector3 m_movement = Vector3.zero;

    // MEMBER FUNCTIONS //

    void Start()
    {
     //   m_transform = transform;
		currentHealth = MaximumHealth;
        playerSpawnPosition = Player.getPlayerGameObject().transform.position;
    }
	
	public static void setPlayerGameObject(GameObject p)
	{
		playerGameObject = p;
	}
	
	public static GameObject getPlayerGameObject()
	{
		return playerGameObject;
	}

    public static void KillPlayer()
    {
        getPlayerGameObject().transform.position = new Vector3(1, 1, 0);
    }

    public static void AddScore(int amount)
    {
        Score += amount;
    }

    public static int GetScore()
    {
        return Score;
    }

    void Update()
    {
     //   CharacterController controller = GetComponent<CharacterController>();

      //  if (controller.isGrounded)
      //  {
      ///      m_movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
     //       m_movement = m_transform.TransformDirection(m_movement);
     //       m_movement *= MovementSpeed;

      //      if (Input.GetButton("Jump"))
     //       {
     //           m_movement.y = JumpSpeed;
     //       }
     //   }
        
        // Add to the Y the gravity value
        // The gravity is stored as a minus value for
        // clarity purposes.
     //   m_movement.y += Gravity * Time.deltaTime;

     //   controller.Move(m_movement * Time.deltaTime);
    }

}
