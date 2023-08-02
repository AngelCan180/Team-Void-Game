using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthbar; 

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;  //Added missing semicolon
        healthbar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(0);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; 
        healthbar.SetHealth(currentHealth); //Corrected typo from colon to semicolon
    }
}
