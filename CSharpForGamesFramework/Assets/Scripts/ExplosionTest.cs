using System.Collections;
using UnityEngine;

public class ExplosionTest : MonoBehaviour
{
    //Make the variable only accept values within a range when in edior
    [SerializeField, Range(0.01f, 1f)] private float m_explosionFrequency = 0.1f;
    [SerializeField, Range(0.5f, 4f)] private float m_explosionRadius = 0.5f;

    private void Awake()
    {
        //Starts another function within the same script
        StartCoroutine(ExplosionSpawner()); 
    }
    //IEnumerator method allows for a function to pause executuion and continue again later

    private IEnumerator ExplosionSpawner()
    {
        //makes it run forever
        while (true)
        {
            //yield return new WaitForSeconds(num) makes makes the code pause for a specified amount of time
            yield return new WaitForSeconds(m_explosionFrequency);

            Vector3 explosionPos = transform.position;
            // will randomise the explosion position to add a random position within a circle based on the radius size 
            explosionPos += (Vector3)UnityEngine.Random.insideUnitCircle.normalized * m_explosionRadius;

            //calls the CreateExplosion() function from VFXManager
            VFXManager.CreateExplosion(explosionPos);
        }
    }
}
