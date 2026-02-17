using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.Controls;

public class NavMeshEnemyScript : MonoBehaviour
{
    public GameObject m_target;
    NavMeshAgent m_agent;

    //Holds the current coroutine being used
    public Coroutine m_behaviourState;
    bool m_playerInSight;

    [SerializeField]private float m_followDistance = 10;
    [SerializeField]private float m_attackDistance = 3;
    [SerializeField] private float m_detectionTime;
    [SerializeField] private float m_idleSpeed;

    [SerializeField] private GameObject m_projectilePrefab;
    [SerializeField] private float m_projectileSpeed;
    [SerializeField] private float m_fireRate;

    public Transform[] m_path;

    enum EnemyState
    {
        Idle,
        Suspicious,
        Follow,
        Attack
    };

    [SerializeField] EnemyState m_enemyState;

    private void Start()
    {
        m_agent = GetComponent<NavMeshAgent>();
        //assigns m_target to the player as it is the only object with the tag "Player"
        m_target = GameObject.FindWithTag("Player");
        UpdateBehaviour(EnemyState.Idle);
    }

    private void Update()
    {
        if (Vector2.Distance(m_target.transform.position, transform.position) > m_followDistance && m_enemyState != EnemyState.Idle)
        {
            UpdateBehaviour(EnemyState.Idle);
        }
        else if (Vector2.Distance(m_target.transform.position, transform.position) <= m_followDistance && m_enemyState != EnemyState.Suspicious && m_playerInSight == false)
        {
            UpdateBehaviour(EnemyState.Suspicious);
        }
        else if (Vector2.Distance(m_target.transform.position, transform.position) <= m_followDistance && Vector2.Distance(m_target.transform.position, transform.position) > m_attackDistance && m_enemyState != EnemyState.Follow && m_playerInSight == true)
        {
            UpdateBehaviour(EnemyState.Follow);
        }
        else if (Vector2.Distance(transform.position, m_target.transform.position) <= m_attackDistance && m_enemyState != EnemyState.Attack)
        {
            UpdateBehaviour(EnemyState.Attack);
        }
    }

    private void UpdateBehaviour(EnemyState state)
    {
        m_enemyState = state;

        if (m_behaviourState != null)
        {
            StopCoroutine(m_behaviourState);
        }
        switch (m_enemyState)
        {
            case EnemyState.Idle:
                m_behaviourState = StartCoroutine(Idle());
                break;
            case EnemyState.Suspicious:
                m_behaviourState = StartCoroutine(Suspicious());
                break;
            case EnemyState.Follow:
                m_behaviourState = StartCoroutine(Follow());
                break;
            case EnemyState.Attack:
                m_behaviourState = StartCoroutine(Attack());
                break;
            default:

                break;
        }
    }

    public IEnumerator Idle()
    {
        int m_currentWaypoint = 0;

        //checks all waypoints on the patrol path array to see which is the closest to the enemy and dessignates that as the new closest path point
        for (int i = 0; i < m_path.Length; i++)
        {
            if (Vector2.Distance(transform.position, m_path[i].position) < Vector2.Distance(transform.position, m_path[m_currentWaypoint].position))
            {
                m_currentWaypoint = i;
            }
        }

        while (true)
        {
            
            m_playerInSight = false;
            m_agent.ResetPath();
            //m_agent.SetDestination(m_path[m_currentWaypoint].position);
            transform.position = Vector2.MoveTowards(transform.position, m_path[m_currentWaypoint].position, m_idleSpeed * Time.deltaTime);
            if ((Vector2)transform.position == (Vector2)m_path[m_currentWaypoint].position)
            {
                if (m_currentWaypoint == m_path.Length - 1)
                {
                    m_currentWaypoint = 0;
                }
                else
                {
                    m_currentWaypoint++;
                }
                yield return new WaitForSeconds(2);
            }
            yield return null;
            
        }
    }

    public IEnumerator Suspicious()
    {
        while (true)
        {
            yield return new WaitForSeconds(m_detectionTime);
            m_playerInSight = true;
        }
    }

    //This will cause the enemy to follow the player after they have entered the detection range and have spent long enough in there
    public IEnumerator Follow()
    {
        while (true)
        {
            m_agent.SetDestination(m_target.transform.position);
            yield return null;
        }
    }

    public IEnumerator Attack()
    {
        while(true)
        {
            Vector2 fireDirection = m_target.transform.position - transform.position;

            GameObject spawnedProjectile = Instantiate(m_projectilePrefab, transform.position, Quaternion.identity);

            Rigidbody2D projectileRB = spawnedProjectile.GetComponent<Rigidbody2D>();
            if (projectileRB != null)
            {
                projectileRB.AddForce(fireDirection.normalized * m_projectileSpeed, ForceMode2D.Impulse);

            }
            yield return new WaitForSeconds(m_fireRate);
        }
    }
}
