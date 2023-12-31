using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Needed to manage scenes

public class HealthBar : MonoBehaviour
{
    public Slider slider;

     public float restartDelay = 3f;

    public Animator animator;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health)
    {
        slider.value = health;

        if (health <= 0)
        {
            // Call a method to restart the game
           animator.SetBool("IsDead", true);
           Invoke("RestartGame", restartDelay);
        }
    }

    private void RestartGame()
    {
        SceneManager.LoadScene("GameOver");
    }
}
