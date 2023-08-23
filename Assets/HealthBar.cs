using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Needed to manage scenes

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    Animator myanimator;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health)
    {
        slider.value = health;

        myanimator.SetTrigger("Hurt");
        if (health <= 0)
        {
            // Call a method to restart the game
            RestartGame();
        }
    }

    private void RestartGame()
    {
        // Going to Game Over Scene
        SceneManager.LoadScene("GameOver");
    }
}
