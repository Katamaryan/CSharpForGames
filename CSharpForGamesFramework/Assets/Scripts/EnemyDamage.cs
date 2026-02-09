using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
        
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy") && !collision.gameObject.CompareTag("Detection"))
        {
            Vector3 explosionPos = transform.position;
            Destroy(gameObject);
            VFXManager.CreateExplosion(explosionPos);
        }
    }
}
