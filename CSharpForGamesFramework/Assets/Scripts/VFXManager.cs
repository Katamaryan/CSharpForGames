using UnityEngine;

public class VFXManager : MonoBehaviour
{
    public static VFXManager s_instance;
    [SerializeField] private GameObject m_explosionPrefab;

    void Awake()
    {
        if (s_instance == null)
        {
            s_instance = this;
        }
    }

    /// <summary>
    /// Creates an explosion at a position and destroys it after time has passed.
    /// </summary>
    /// <param name="position"> The position the explosion should spawn</param>
    /// <param name="destroyAfter"> the duration of the explosion before destruction</param>
    /// <returns></returns>
    // Update is called once per frame
    public static GameObject CreateExplosion(Vector3 position, float destroyAfter = 0.5f)
    {
        //If instance isn't set then return null
        if (s_instance == null)
        {
            Debug.LogError("Tried to spawn an explosion but the instance hasn't been set.");
            return null;
        }

        //Otherwise, spawn the explosion
        GameObject explosion = Instantiate(s_instance.m_explosionPrefab, position, Quaternion.identity);

        //Destroy the explosion after a certain amount of time.
        Destroy(explosion, destroyAfter);

        //Return a reference to the explosion
        return explosion;
    }
}
