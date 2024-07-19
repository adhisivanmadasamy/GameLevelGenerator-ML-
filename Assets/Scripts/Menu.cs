using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using static System.Net.WebRequestMethods;

public class Menu : MonoBehaviour
{
    public GameObject MenuPanel, HUDPanel;
    public GameObject SelectPanel;
    public FlaskReq flaskReq;

    public TextMeshProUGUI roomText, dungeonText;
    public void ClickPlay()
    {
        SelectPanel.SetActive(true);
        MenuPanel.SetActive(false);
    }

    public void Generate()
    {
        SelectPanel.SetActive(false);
        HUDPanel.SetActive(true);
        flaskReq.GetDungeonData();
    }

    public void QuitApp()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ChooseDungeonVAE()
    {
        flaskReq.Dungeon_url = "http://127.0.0.1:5000/getDungeonVAE";
        dungeonText.text = "VAE";
    }
    public void ChooseDungeonGAN()
    {
        flaskReq.Dungeon_url = "http://127.0.0.1:5000/getDungeonGAN";
        dungeonText.text = "GAN";
    }

    public void ChooseRoomCGAN()
    {
        flaskReq.Room_url = "http://127.0.0.1:5000/getRoomCGAN";
        roomText.text = "CGAN";
    }
    public void ChooseRoomVAE()
    {
        flaskReq.Room_url = "http://127.0.0.1:5000/getRoomVAE";
        roomText.text = "VAE";
    }

    public void backtoMain()
    {
        SelectPanel.SetActive(false);
        MenuPanel.SetActive(true);
    }
}
