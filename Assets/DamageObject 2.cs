using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObject : MonoBehaviour
{
    public int damageAmount = 20;

    void OnTriggerEnter2D(Collider2D other)  // Changed to 2D version
    {
        if (other.gameObject.CompareTag("player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(damageAmount);
            }
        }
    }
}
