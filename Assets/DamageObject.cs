using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObject : MonoBehaviour
{
    public int initialDamage = 20;
    public int recurringDamage = 10;
    public float recurringDamageInterval = 3f;

    private bool isPlayerColliding = false;
    private float lastRecurringDamageTime;

    public AudioClip damageSound;
    public AudioSource audioSource;

    void Start()
    {
        lastRecurringDamageTime = Time.time;
    }

    void Update()
    {
        if (isPlayerColliding)
        {
            if (Time.time - lastRecurringDamageTime >= recurringDamageInterval)
            {
                lastRecurringDamageTime = Time.time;
                InflictRecurringDamage();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(initialDamage);
                Animator anim = player.gameObject.GetComponent<Animator>();
                anim.SetTrigger("Hurt");
                isPlayerColliding = true;

                // Play the initial damage sound.
                audioSource.PlayOneShot(damageSound);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            isPlayerColliding = false;
            Animator anim = other.gameObject.GetComponent<Animator>();
                anim.ResetTrigger("Hurt");
        }
    }

    void InflictRecurringDamage()
    {
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            player.TakeDamage(recurringDamage);
            Animator anim = player.gameObject.GetComponent<Animator>();
                anim.SetTrigger("Hurt");

            // Play the recurring damage sound.
            audioSource.PlayOneShot(damageSound);
        }
    }
}
