using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject GameOverPanel;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Yes()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void No()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
