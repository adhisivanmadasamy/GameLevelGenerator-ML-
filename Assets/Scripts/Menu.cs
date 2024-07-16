using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject MenuPanel, HUDPanel;
    public void ClickPlay()
    {
        HUDPanel.SetActive(true);
        MenuPanel.SetActive(false);
    }

    public void QuitApp()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
