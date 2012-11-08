using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
    // PUBLIC PROPERTIES //

    public float MovementSpeed = 6.0f;
    public float JumpSpeed = 8.0f;
    public float Gravity = -9.81f;

    // PRIVATE VARIABLES //

    private Transform m_transform;
    private Vector3 m_movement = Vector3.zero;

    // MEMBER FUNCTIONS //

    void Start()
    {
        m_transform = transform;
    }

    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();

        if (controller.isGrounded)
        {
            m_movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            m_movement = m_transform.TransformDirection(m_movement);
            m_movement *= MovementSpeed;

            if (Input.GetButton("Jump"))
            {
                m_movement.y = JumpSpeed;
            }
        }
        
        // Add to the Y the gravity value
        // The gravity is stored as a minus value for
        // clarity purposes.
        m_movement.y += Gravity * Time.deltaTime;

        controller.Move(m_movement * Time.deltaTime);
    }

}
