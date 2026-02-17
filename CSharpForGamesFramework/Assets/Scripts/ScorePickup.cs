using UnityEngine;

public class ScorePickup : MonoBehaviour
{
    public GameObject m_scoreManager;
    public ScoreSystem m_scoreSystem;
    private void Start()
    {
        m_scoreManager = GameObject.FindWithTag("ScoreSystem");
        m_scoreSystem = m_scoreManager.GetComponent<ScoreSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            m_scoreSystem.AddScore(10);
            Destroy(gameObject);
        }
    }
}
