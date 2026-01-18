using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject m_menuPanel;
    [SerializeField] private GameObject m_controlsPanel;
    [SerializeField] private GameObject m_pausePanel;
    private bool m_controlsPanelOpen = false;
    private bool m_pausePanelOpen = false;

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

    public void Quit()
    {
        Application.Quit();
    }
}
