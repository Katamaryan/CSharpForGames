using UnityEngine;

public class ScorePickup : MonoBehaviour
{
    public ScoreSystem scoreSystem;
    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            scoreSystem.AddScore(10);
            Destroy(gameObject);
        }
    }
}
