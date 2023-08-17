using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
   public GameObject PausePanel;

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Pause()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0;
    }
     public void Resume()
     {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
     }
     public void Exit()
     {
        SceneManager.LoadScene("Main Menu");
     }
     public void Restart()
     {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        print("The button is working");
     }

}