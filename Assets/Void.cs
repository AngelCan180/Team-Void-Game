using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Void : MonoBehaviour
{
    static int maxHealth = 100;
    static int currentHealth;

    static void Start()
    {
        currentHealth = maxHealth;
    }

    public static void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log("Player took " + damageAmount + " damage. Current health: " + currentHealth);
    }

    public static void DealDamageToPlayer(int damage)
    {
        TakeDamage(damage);
    }
}
