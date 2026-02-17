using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    private float m_damage = 15;
    Health m_player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") && !collision.CompareTag("Detection"))
        {
            if (collision.CompareTag("Player"))
            {
                m_player = collision.GetComponent<Health>();
                m_player.m_health -= m_damage;
            }
            
            Vector3 explosionPos = transform.position;
            Destroy(gameObject);
            VFXManager.CreateExplosion(explosionPos);
            
        }
    }
}
