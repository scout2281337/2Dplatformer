using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : Singleton<MenuManager>
{
    public GameObject menu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && SceneManager.GetActiveScene().name == "TestRoom")
        {
            SceneTransition.SwitchToScene("Mainmenu");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    public void GoToGame()
    {
        SceneTransition.SwitchToScene("Mainmenu");
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void StartGame()
    {
        SceneTransition.SwitchToScene("TestRoom");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ToggleMenu()
    {
        menu.SetActive(!menu.activeSelf);
    }
}
