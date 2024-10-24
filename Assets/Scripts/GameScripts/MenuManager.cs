using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject menu;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ( Input.GetKeyDown(KeyCode.P) && SceneManager.GetActiveScene().name == "TestRoom"  )
        {
            SceneTransition.SwitchToScene("Mainmenu");
        }
        if ( Input.GetKeyDown(KeyCode.Escape)) 
        {

            menu.SetActive(!menu.activeSelf);
            
        
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

    public void OpenMenu() 
    {
        menu.SetActive(!menu.activeSelf);
    }

    public void StartGame()
    {
        SceneTransition.SwitchToScene("TestRoom");

    }

    public void QuitGame()
    {
        Application.Quit();

    }
}
