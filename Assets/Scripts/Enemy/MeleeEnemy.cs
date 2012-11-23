using UnityEngine;
using System.Collections;

public class MeleeEnemy : MonoBehaviour, IEnemy 
{

    public enum EnemyState
    {
        ENEMY_PATROLLING,
        ENEMY_AGGRO,
        ENEMY_ATTACKING,
        ENEMY_DEAD,
        ENEMY_EVADE
    }

    // Threat related variables
    public float AggroRadius = 2;
    public float AttackRadius = 0.25f;
    public float PlayerChaseRadius = 6;

    // Enemy specific variables
    public int Health = 100;
    public int Damage = 25;
    public float MovementSpeed = 0.75f;

    // Enemy state
    public EnemyState enemyState;

    // Timing variable
    private float startTime = 0.0f;

    private Transform playerTransform;

    private bool inCombat = false;

	// Use this for initialization
	void Start () 
    {
        startTime = Time.time;
        enemyState = EnemyState.ENEMY_PATROLLING;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (enemyState == EnemyState.ENEMY_PATROLLING)
        {
            if( Vector3.Distance( transform.position, playerTransform.position ) < AggroRadius )
            {
                // Ouch, we've pulled it!
                inCombat = true;
            }
        }

        if (inCombat)
            InCombat();
	}

    public void InCombat()
    {
        PatrolPath enemyPatrol = GetComponent<PatrolPath>() as PatrolPath;

        // Turn towards player
        if (enemyState == EnemyState.ENEMY_PATROLLING)
        {
            // Stop the enemy from Patrolling
            enemyPatrol.Disable();

            enemyState = EnemyState.ENEMY_AGGRO;
        }

        if (enemyState == EnemyState.ENEMY_AGGRO)
        {
            // Change direction towards player
            if (ChangeDirection(playerTransform.position))
            {
                // Walk towards the player
                Vector3 faceDirection = playerTransform.position - transform.position;
                transform.position = transform.position + (faceDirection * (MovementSpeed * Time.smoothDeltaTime));

                if (Vector3.Distance(transform.position, playerTransform.position) <= AttackRadius)
                {
                    enemyState = EnemyState.ENEMY_ATTACKING;
                }
            }
        }

        if (enemyState == EnemyState.ENEMY_ATTACKING)
        {
            if (Vector3.Distance(transform.position, playerTransform.position) >= AttackRadius)
            {
                enemyState = EnemyState.ENEMY_AGGRO;
            }
        }

        if (enemyState == EnemyState.ENEMY_EVADE)
        {
            // Move back towards the disabled position
            if (ChangeDirection(enemyPatrol.GetDisabledPosition()))
            {
                Vector3 faceDirection = enemyPatrol.GetDisabledPosition() - transform.position;
                transform.position = transform.position + (faceDirection * (MovementSpeed * Time.smoothDeltaTime));

                if (Vector3.Distance(transform.position, enemyPatrol.GetDisabledPosition()) < 0.01f)
                {
                    enemyState = EnemyState.ENEMY_PATROLLING;
                    enemyPatrol.Enable();
                    inCombat = false;
                }
            }
        }

        if (Vector3.Distance(transform.position, enemyPatrol.GetDisabledPosition()) >= PlayerChaseRadius)
        {
            enemyState = EnemyState.ENEMY_EVADE;
        }
    }

    bool ChangeDirection(Vector3 target)
    {
        Vector3 faceDirection = target - transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(faceDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, (Time.time - startTime) * 3);

        if (Quaternion.Angle(transform.rotation, targetRotation) < 0.01f)
        {
            return true;
        }

        return false;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, AggroRadius);

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(GetComponent<PatrolPath>().GetDisabledPosition(), PlayerChaseRadius);
    }
}
