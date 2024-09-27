using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //private AudioSource asss;
    void Start()
    {
        //asss = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //asss.
    }


    public void StartGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    
    }

    public void QuitGame() 
    {
        Application.Quit();
    
    }
}
