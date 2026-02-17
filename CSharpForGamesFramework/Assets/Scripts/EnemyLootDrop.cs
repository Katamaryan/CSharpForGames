using UnityEngine;

public class EnemyLootDrop : MonoBehaviour
{
    public bool m_beenRobbed;
    NavMeshEnemyScript m_enemyScript;
    private void Start()
    {
        m_enemyScript = GetComponent<NavMeshEnemyScript>();
    }
    // Update is called once per frame
    void Update()
    {
        if (m_beenRobbed)
        {
            m_enemyScript.m_beenRobbed = true;
            m_beenRobbed = false;
        }
    }
}
