using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [SerializeField] private Transform m_startWaypoint;
    [SerializeField] private Transform m_endWaypoint;
    [SerializeField] private float m_speed = 5;

    private Transform m_target;
    private Rigidbody2D m_rigidbody; // calls the component attached to the object

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_target = m_startWaypoint;

    }

    // Update is called once per frame
    void Update()
    {
        //basic way of moving obstacle
        //Vector2 position = transform.position;
        //position.y = transform.position.y + 0.1f*Time.deltaTime; // im fairly sure the delta time thing is to make it so it is consistent no matter the tick speed
        //transform.position = position;

        //cooler way of moving obstacle
        //get the vector to the target
        //Vector2 direction = m_target.position - transform.position;
        //apply movement in direction
        //m_rigidbody.linearVelocity = direction.normalized * m_speed * Time.deltaTime;

        //another way of moving
        //Vector2 direction = m_target.position - transform.position;
        //transform.Translate(direction * m_speed * Time.deltaTime);

        //final way of moving, this one moves to a location of stops once reached
        transform.position = Vector2.MoveTowards(transform.position, m_target.position, m_speed * Time.deltaTime);
        transform.Rotate(Vector3.forward * 100 * Time.deltaTime);
    }

    void ChangeTarget()
    {
        if (m_target == m_startWaypoint) m_target = m_endWaypoint;

        else if (m_target = m_endWaypoint) m_target = m_startWaypoint;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MovingObstacleWaypoint"))
        {
            ChangeTarget();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hit");
        }
    }
}
