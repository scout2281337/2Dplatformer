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
        SceneTransition.SwitchToScene("SampleScene");

    }

    public void QuitGame() 
    {
        Application.Quit();
    
    }
}
