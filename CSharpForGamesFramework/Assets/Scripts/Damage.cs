using UnityEngine;

public class Damage : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(!collision.gameObject.CompareTag("Player"))
        {
            Vector3 explosionPos = transform.position;
            Destroy(gameObject);
            VFXManager.CreateExplosion(explosionPos);
        }
    }
}
