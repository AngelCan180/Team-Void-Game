using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObject : MonoBehaviour
{
    public int damageAmount = 20;

    public AudioClip damageSound;  // Clip to play when the player takes damage.
    public AudioSource audioSource;  // Source from which to play the clip.

    void OnTriggerEnter2D(Collider2D other)  // Changed to 2D version
    {
        if (other.gameObject.CompareTag("player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(damageAmount);

                // Play the damage sound.
                audioSource.PlayOneShot(damageSound);
            }
        }
    }
}
