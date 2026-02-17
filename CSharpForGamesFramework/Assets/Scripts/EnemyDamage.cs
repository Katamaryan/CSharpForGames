using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    private float m_damage = 5;
    Health player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy") && !collision.gameObject.CompareTag("Detection"))
        {
            player = collision.gameObject.GetComponent<Health>();
            Vector3 explosionPos = transform.position;
            Destroy(gameObject);
            VFXManager.CreateExplosion(explosionPos);
            player.m_health -= m_damage;
        }
    }
}
