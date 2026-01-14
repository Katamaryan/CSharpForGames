using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    // The Current score of the player
    public int m_score;

    /// <summary>
    /// Method to add a given amount of score to the player
    /// </summary>
    /// <param name="scoreToAdd">The amount of score to be added.</param>

    public void AddScore(int scoreToAdd)
    {
        //Increment the player score by the amount to add.
        m_score += scoreToAdd;
    }

    //public void Update()
    //{
    //    m_score += 10;
    //}
}
