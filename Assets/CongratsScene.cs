using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CongratsScene : MonoBehaviour
{
    public GameObject CongratsPanel;
    // Update is called once per frame
    void Update()
    {
        
    } 
        
    public void BackToMain()
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1;
    }
    

}
