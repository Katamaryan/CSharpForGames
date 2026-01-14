using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public ScoreSystem m_scoreSystem;
    public TMPro.TextMeshProUGUI m_uiLabel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_uiLabel.text = "Score: " + m_scoreSystem.m_score;
    }
}
