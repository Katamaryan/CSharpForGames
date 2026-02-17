using UnityEngine;

public class Damage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(!collision.CompareTag("Player") && !collision.CompareTag("Projectile") && !collision.CompareTag("Detection") && !collision.CompareTag("ScorePickup"))
        {
            if (collision.CompareTag("Enemy"))
            {
                NavMeshEnemyScript m_enemyLoot = collision.GetComponent<NavMeshEnemyScript>();
                m_enemyLoot.m_beenRobbed = true;
            }
            Vector3 explosionPos = transform.position;
            
            Destroy(gameObject);
            VFXManager.CreateExplosion(explosionPos);
            
        }
    }
}
