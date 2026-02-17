using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public MenuManager m_menuManager;
    public float m_health;
    private float m_maxHealth = 100;
    public Image m_healthBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        m_menuManager = GameObject.FindWithTag("Menu").GetComponent<MenuManager>();
        m_health = m_maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        m_healthBar.fillAmount = m_health / m_maxHealth;

        if (m_health <= 0)
        {
            m_menuManager.ToggleDeathMenu();
            Destroy(gameObject);
        }
    }

}
