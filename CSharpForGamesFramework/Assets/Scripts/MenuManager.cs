using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject m_menuPanel;
    [SerializeField] private GameObject m_controlsPanel;
    [SerializeField] private GameObject m_pausePanel;
    [SerializeField] private GameObject m_deathPanel;
    private bool m_controlsPanelOpen = false;
    private bool m_pausePanelOpen = false;
    private bool m_deathPanelOpen = false;

    public void LoadLevel1()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ToggleControlsPanel()
    {
        if (m_controlsPanelOpen)
        {
            m_menuPanel.SetActive(true);
            m_controlsPanel.SetActive(false);
        }
        else
        {
            m_menuPanel.SetActive(false);
            m_controlsPanel.SetActive(true);
        }
        m_controlsPanelOpen = !m_controlsPanelOpen;
    }

    public void TogglePausePanel()
    {
        if (m_pausePanelOpen)
        {
            m_pausePanel.SetActive(false);
        }
        else
        {
            m_pausePanel.SetActive(true);
        }
        m_pausePanelOpen = !m_pausePanelOpen;
    }
    public void ToggleDeathMenu()
    {
        if (m_deathPanelOpen)
        {
            m_deathPanel.SetActive(false);
        }
        else
        {
            m_deathPanel.SetActive(true);
        }
        m_deathPanelOpen = !m_deathPanelOpen;
    }
    public void Quit()
    {
        Application.Quit();
    }
}
