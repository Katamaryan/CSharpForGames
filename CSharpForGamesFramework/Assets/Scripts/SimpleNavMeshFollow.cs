using UnityEngine;
using UnityEngine.AI;

public class SimpleNavMeshFollow : MonoBehaviour
{
    public Transform m_target;
    NavMeshAgent m_agent;

    [SerializeField] private float m_stoppingDistance;
    bool m_PlayerInSight;

    [SerializeField] private GameObject m_projectilePrefab;
    [SerializeField] private float m_projectileSpeed;
    [SerializeField] float m_fireRate;
    private float m_fireTimeout = 0;

    enum EnemyStates
    {
        Idle,
        MoveToPlayer,
        Attack
    };

    EnemyStates m_enemyStates = EnemyStates.Idle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_agent = GetComponent<NavMeshAgent>();
        m_target = FindObjectOfType<TopDownCharacterController>().transform;

    }

    // Update is called once per frame
    void Update()
    {
        switch (m_enemyStates)
        {
            case EnemyStates.Idle:

                break;
            case EnemyStates.MoveToPlayer:
                m_agent.SetDestination(m_target.position);
                break;
            case EnemyStates.Attack:

                if (Time.time > m_fireTimeout)
                {
                    m_fireTimeout = Time.time + m_fireRate;
                    Vector2 fireDirection = m_target.position - transform.position;

                    GameObject spawnedProjectile = Instantiate(m_projectilePrefab, transform.position, Quaternion.identity);

                    Rigidbody2D projectileRB = spawnedProjectile.GetComponent<Rigidbody2D>();
                    if (projectileRB != null)
                    {
                        projectileRB.AddForce(fireDirection.normalized * m_projectileSpeed, ForceMode2D.Impulse);

                    }
                }
                break;
            default:
                break;
        }


        if (!m_PlayerInSight)
        {
            m_enemyStates = EnemyStates.Idle;
        }
        if (Vector2.Distance(transform.position, m_target.position) <= m_stoppingDistance)
        {
            m_enemyStates = EnemyStates.Attack;
        }
        if (m_PlayerInSight && Vector2.Distance(transform.position, m_target.position) >= m_stoppingDistance)
        {
            m_enemyStates = EnemyStates.MoveToPlayer;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == GameObject.Find("Character"))
        {
            m_PlayerInSight = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == GameObject.Find("Character"))
        {
            m_PlayerInSight = false;
        }
    }
}
