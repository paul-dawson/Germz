/*
 * ===== GERMZ ===========================================
 * 
 * PatrolPathing
 * -------------------------------------------------------
 * Implements the ability for GameObjects to move along
 * a desired path
 * -------------------------------------------------------
 * @author Paul Dawson <k0066812>
 * @created 08-Nov-2012 17:14
 * @version 1 (08-Nov-2012 ----- by @author)
 *
 */

using UnityEngine;
using System.Collections;

public class PatrolPath : MonoBehaviour 
{

    //
    // Enumerators
    //

    public enum PatrolState
    {
        PATROL_STOPPED,
        PATROL_WALKING,
        PATROL_ROTATING
    };

    // 
    // Public Properties
    //

    // How fast the assigned GameObject will move along its designated patrol path
    public float PatrolSpeed;

    // How fast the GameObject will rotate (upon completing a path, or rotating towards its enemy (the player))
    public float PatrolRotationSpeed;

    // An array of assigned waypoints to move between
    public Vector3[] Waypoints;

    // Is the patrol path active?
    public bool Active = true;

    // Allows for paths to be shown in the editor
    public bool ShowPath = true;

    // The current patrol state of the GO
    public PatrolState CurrentState = PatrolState.PATROL_STOPPED;

    // How far should the GO be away from the waypoint before it is considered to have reached that waypoint
    public float WaypointBias = 0.05f;

    //
    // Private Member Variables
    //

    // The target waypoint the GameObject is moving towards.
    private int m_targetWaypoint = 0;

    // The current waypoint the GameObject is moving towards.
    private int m_currentWaypoint = 0;

    // Is the GameObject moving through towards the last waypoint? (if wrapping is false, the GO should move back along the path)
    private bool m_movingForward = true;

    // The desired direction that the GO should face
    private Vector3 m_faceDirection = Vector3.zero;

    // Timing variable
    private float m_startTime = 0.0f;

    // Stores the position when disabled so when it is reenabled it can return to its previous position
    private Vector3 m_disabledPosition = Vector3.zero;

    //
    // Member Functions
    //

    /* Performs a basic sanity check to ensure we've setup the variables in Unity correctly */
	void Start () 
    {
        // Ensure our Waypoint list has at least two entries
        if (!WaypointSanityCheck())
            return;

        // Ensure our GameObject has been assigned and is valid
        if (gameObject.active != true)
        {
            Debug.Log("PatrolPath: Unable to start GO Patrol (Assigned GameObject is invalid, or is not active)");
            Active = false;
            return;
        }

        // The GO passed the sanity check; start patrolling!
        m_startTime = Time.time;
        CurrentState = PatrolState.PATROL_WALKING;

        m_currentWaypoint = 0;
        m_targetWaypoint = 1;

        //Debug.Log("PatrolPath: moving to waypoint: " + (m_targetWaypoint));
	}

    /* Very simple update function that starts the objects patrol */
	void LateUpdate () 
    {
        if (Active)
        {
            Patrol();
        }        
	}

    /* The patrol function that will move the GO towards its target waypoint */
    private void Patrol()
    {
        if (!WaypointSanityCheck())
            return;

        // Calculate the distance between the current and next waypoint
        // If the distance is less than one metre, jump to the next waypoint and carry on!
        if (Vector3.Distance(transform.position, Waypoints[m_targetWaypoint]) < WaypointBias)
        {
            if (m_movingForward)
            {
                m_currentWaypoint++;
                m_targetWaypoint++;
            }
            else
            {
                m_currentWaypoint--;
                m_targetWaypoint--;
            }

            //Debug.Log("Reached destination, moving to waypoint: " + (m_targetWaypoint));

            if (m_movingForward && m_targetWaypoint >= Waypoints.Length)
            {
                m_movingForward = false;
                m_currentWaypoint = Waypoints.Length - 1;
                m_targetWaypoint = m_currentWaypoint - 1;

                //Debug.Log("Reached end, moving back to waypoint: " + (m_targetWaypoint));

                CurrentState = PatrolState.PATROL_ROTATING;
            }

            if (!m_movingForward && m_targetWaypoint < 0)
            {
                m_movingForward = true;
                m_currentWaypoint = 0;
                m_targetWaypoint = 1;

                CurrentState = PatrolState.PATROL_ROTATING;
            }

            m_startTime = Time.time;
        }

        if(CurrentState == PatrolState.PATROL_WALKING)
            MoveTowards(Waypoints[m_currentWaypoint], Waypoints[m_targetWaypoint]);

        if (CurrentState == PatrolState.PATROL_ROTATING)
            ChangeDirection(Waypoints[m_targetWaypoint]);
    }

    /* Performs a simple sanity check to ensure we don't get any nasty errors */
    private bool WaypointSanityCheck()
    {
        if (Waypoints.Length < 2)
        {
            Debug.Log("PatrolPath: Unable to start GO Patrol (Not enough waypoints: need " + (2 - Waypoints.Length) + " more)");
            Active = false;
            return false;
        }

        return true;
    }

    /* Calculates for the GO to move towards the designated waypoint */
    private void MoveTowards(Vector3 current, Vector3 target)
    {
        // Compute a bias based on the amount of distance that the GO can be away from the waypoint (stops 'jumping' error)
        Vector3 waypointBias = (m_movingForward) ? (Vector3.right * WaypointBias) : (Vector3.left * WaypointBias);

        transform.position = Vector3.Lerp(current + waypointBias, target, (Time.time - m_startTime) * PatrolSpeed);
    }

    /* Rotates the GO towards the designated waypoint
    private void RotateTowards(Vector3 target)
    {
        m_faceDirection = target - transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(m_faceDirection);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, (Time.time - m_startTime) * PatrolRotationSpeed);

        if (transform.rotation == targetRotation)
        {
            CurrentState = PatrolState.PATROL_WALKING;
            m_startTime = Time.time;
        }
    }*/

    bool ChangeDirection(Vector3 target)
    {
        Vector3 faceDirection = target - transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(faceDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * 3);

        if (Quaternion.Angle(transform.rotation, targetRotation) < 0.01f)
        {
            return true;
        }

        return false;
    }

    /* Adds a Sphere GameObject on the set waypoints and draws a line between them to show the path that the GameObject should take */
    private void DrawDebugLines()
    {
        int currentWaypoint = 0;
        foreach (Vector3 waypoint in Waypoints)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawSphere(waypoint, 0.1f);

            if (currentWaypoint < Waypoints.Length-1)
            {
                Gizmos.DrawLine(waypoint, Waypoints[currentWaypoint + 1]);
            }

            currentWaypoint++;
        }
    }

    /* Disables Patrol Pathing and stores the position where disabled from */
    public void Disable()
    {
        Active = false;
        m_disabledPosition = this.gameObject.transform.position;
    }

    /* Reactivates the PatrolPath after Disable() */
    public void Enable()
    {
        this.gameObject.transform.position = m_disabledPosition;
        Active = true;
    }

    /* Returns the position that the PatrolPath was disabled at */
    public Vector3 GetDisabledPosition()
    {
        return m_disabledPosition;
    }

    void OnDrawGizmosSelected()
    {
        if (ShowPath)
            DrawDebugLines();

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, WaypointBias);
    }
}
