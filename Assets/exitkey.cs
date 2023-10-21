using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exitkey : MonoBehaviour
{
    public GameObject Door;
    public Sprite blackColourSprite; // The new sprite
    private AudioSource audioSource;
    private bool keyPickedUp = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("player") && !keyPickedUp)
        {
            Debug.Log("Key Picked up");
            Door.GetComponent<BoxCollider2D>().enabled = true; 

            // play sound
            audioSource.Play();

            keyPickedUp = true;

            // Change the door sprite
            SpriteRenderer doorSpriteRenderer = Door.GetComponent<SpriteRenderer>();
            doorSpriteRenderer.sprite = blackColourSprite;

            // Adjust the sprite to the size of the collider
            BoxCollider2D doorCollider = Door.GetComponent<BoxCollider2D>();
            Vector2 colliderSize = doorCollider.size;
            doorSpriteRenderer.gameObject.transform.localScale = new Vector3(colliderSize.x / doorSpriteRenderer.sprite.bounds.size.x, colliderSize.y / doorSpriteRenderer.sprite.bounds.size.y, 1);

            // Optional: Disable the key's collider or the key itself
            // GetComponent<Collider2D>().enabled = false; // disable collider
            // gameObject.SetActive(false); // disable key
        }
    }
}
