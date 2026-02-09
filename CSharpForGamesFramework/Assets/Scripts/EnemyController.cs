using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform m_Player;
    public float m_speed;
    public float m_stoppingDistance;
    bool m_PlayerInSight;

    [SerializeField] private GameObject m_projectilePrefab;
    [SerializeField] private float m_projectileSpeed;
    [SerializeField] float m_fireRate;
    private float m_fireTimeout = 0;


    //[SerializeField, Range(1f, 10f)] private float m_idleRadius;
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
        m_Player = FindObjectOfType<TopDownCharacterController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        

            switch (m_enemyStates)
        {
            case EnemyStates.Idle:

                //Vector3 idlePos = transform.position;
                //idlePos += (Vector3)UnityEngine.Random.insideUnitCircle.normalized * m_idleRadius;

                //transform.position = Vector2.MoveTowards(transform.position, idlePos, m_speed * Time.deltaTime);
                
                //if (transform.position == idlePos)
                //{
                //    idlePos = transform.position;
                //    idlePos += (Vector3)UnityEngine.Random.insideUnitCircle.normalized * m_idleRadius;
                //}

                break;
            case EnemyStates.MoveToPlayer:
                transform.position = Vector2.MoveTowards(transform.position, m_Player.position, m_speed * Time.deltaTime);
                break;
            case EnemyStates.Attack:

                if (Time.time > m_fireTimeout)
                {
                    m_fireTimeout = Time.time + m_fireRate;
                    Vector2 fireDirection = m_Player.position - transform.position;

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

        if (m_PlayerInSight && Vector2.Distance(transform.position, m_Player.position) >= m_stoppingDistance)
        {
            m_enemyStates = EnemyStates.MoveToPlayer;
        }
        if (!m_PlayerInSight)
        {
            m_enemyStates = EnemyStates.Idle;
        }
        if (Vector2.Distance(transform.position, m_Player.position) <= m_stoppingDistance)
        {
            m_enemyStates = EnemyStates.Attack;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        m_PlayerInSight = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        m_PlayerInSight = false;   
    }
}
